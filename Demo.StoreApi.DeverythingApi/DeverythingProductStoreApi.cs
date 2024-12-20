using Demo.StoreApi.Abstractions;
using Demo.StoreApi.DeverythingApi.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace Demo.StoreApi.DeverythingApi;

internal class DeverythingProductStoreApi : ICandidateProductStoreApi
{
    private readonly IConfiguration _config;

    public DeverythingProductStoreApi(IConfiguration config)
    {
        _config = config;
    }

    public async Task<CheckoutSummary> CheckoutAsync(int boxId, int[] productIds)
    {
        using var client = CreateClient();
        var response = await client.PostAsJsonAsync("checkout", new { boxId, productIds });
        if (!response.IsSuccessStatusCode)
            throw await CreateException(response);

        var checkoutResponse = await response.Content.ReadFromJsonAsync<CheckoutResponse>();

        if (checkoutResponse == null)
            throw new InvalidOperationException($"Deverything API Checkout gave no response for box {boxId} and products {string.Join(", ", productIds)}.");

        if (checkoutResponse.StatusCode != 200)
            throw new InvalidOperationException($"Deverything API Checkout gave unexpected status code \"{checkoutResponse.StatusCode}\" for box {boxId} and products {string.Join(", ", productIds)}.");

        if (string.IsNullOrWhiteSpace(checkoutResponse.Result))
            throw new InvalidOperationException($"Deverything API Checkout gave status code \"{checkoutResponse.StatusCode}\" but no result string for box {boxId} and products {string.Join(", ", productIds)}.");

        var checkoutSummary = new CheckoutSummary
        {
            Result = checkoutResponse.Result,
        };

        return checkoutSummary;
    }

    public async Task<IList<Box>> GetBoxesAsync()
    {
        using var client = CreateClient();
        var response = await client.GetAsync("boxes");
        if (!response.IsSuccessStatusCode)
            throw await CreateException(response);

        var boxesResponse = await response.Content.ReadFromJsonAsync<BoxesResponse>();

        if (boxesResponse == null)
            throw new InvalidOperationException($"Deverything API boxes gave no response.");

        if (boxesResponse.StatusCode != 200)
            throw new InvalidOperationException($"Deverything API boxes gave unexpected status code \"{boxesResponse.StatusCode}\".");

        var boxes = boxesResponse.Boxes?
            .Select(box => new Box
            {
                Id = box.Id,
                Height = box.Height,
                Width = box.Width
            })
            .ToArray() ?? [];

        return boxes;
    }

    public async Task<ProductDimensions> GetProductDimensionsAsync(int productId)
    {
        using var client = CreateClient();
        var response = await client.GetAsync($"products/{productId}");
        if (!response.IsSuccessStatusCode)
            throw await CreateException(response);

        var dimensionsResponse = await response.Content.ReadFromJsonAsync<ProductDimensionsResponse>();

        if (dimensionsResponse == null)
            throw new InvalidOperationException($"Deverything API product dimensions gave no response.");

        if (dimensionsResponse.StatusCode != 200)
            throw new InvalidOperationException($"Deverything API product dimensions gave unexpected status code \"{dimensionsResponse.StatusCode}\".");

        var dimensions = new ProductDimensions
        {
            ProductId = productId,
            Height = dimensionsResponse.Height,
            Width = dimensionsResponse.Width,
        };

        return dimensions;
    }

    public async Task<IList<Product>> GetProductsAsync()
    {
        using var client = CreateClient();
        var response = await client.GetAsync("products");
        if (!response.IsSuccessStatusCode)
            throw await CreateException(response);

        var productsResponse = await response.Content.ReadFromJsonAsync<ProductsResponse>();

        if (productsResponse == null)
            throw new InvalidOperationException($"Deverything API products gave no response.");

        if (productsResponse.StatusCode != 200)
            throw new InvalidOperationException($"Deverything API products gave unexpected status code \"{productsResponse.StatusCode}\".");

        var products = productsResponse.Products?
            .Select(product => new Product
            {
                Id = product.Id,
                Name = product.Name ?? string.Empty,
                Price = product.Price
            })
            .ToArray() ?? [];

        return products;
    }

    private async Task<Exception> CreateException(HttpResponseMessage response)
    {
        var errorMessage = await response.Content.ReadAsStringAsync();
        return new InvalidOperationException($"{nameof(DeverythingProductStoreApi)} failed external api call to {response.RequestMessage?.Method.ToString() ?? "<?>"} \"{response.RequestMessage?.RequestUri?.ToString() ?? "<unknown>"}\" with status code {response.StatusCode} and message \"{errorMessage}\".");
    }

    private HttpClient CreateClient()
    {
        var client = new HttpClient();
        client.Timeout = TimeSpan.FromSeconds(10);
        client.BaseAddress = new Uri(_config[ConfigurationConstants.BaseUrl] ?? throw new InvalidOperationException($"\"{ConfigurationConstants.BaseUrl}\" configuration is missing."));
        client.DefaultRequestHeaders.Add("USER", _config[ConfigurationConstants.User] ?? throw new InvalidOperationException($"\"{ConfigurationConstants.User}\" configuration is missing."));
        client.DefaultRequestHeaders.Add("APIKEY", _config[ConfigurationConstants.ApiKey] ?? throw new InvalidOperationException($"\"{ConfigurationConstants.ApiKey}\" configuration is missing."));
        return client;
    }
}
