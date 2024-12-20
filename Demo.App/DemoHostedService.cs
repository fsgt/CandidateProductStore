using Demo.Abstractions;
using Microsoft.Extensions.Hosting;

namespace Demo.App;

internal class DemoHostedService : BackgroundService
{
    private readonly ICandidateProductStore _candidateProductStore;

    public DemoHostedService(ICandidateProductStore candidateProductStore)
    {
        _candidateProductStore = candidateProductStore;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Step 1.
        var allProducts = await _candidateProductStore.GetAllProductsAsync();

        // Step 2.
        var productsWithPriceAbove300 = await _candidateProductStore.GetAllProductsAbovePriceAsync(300m);

        // Step 3 & 4.
        var product3 = allProducts.Single(product => product.Id == 3);
        var product7 = allProducts.Single(product => product.Id == 7);
        var box = await _candidateProductStore.CalculateSmallestBoxForTwoProductsAsync(product3, product7);

        // Step 5.
        var checkoutSummary = await _candidateProductStore.CheckoutAsync(box, product3, product7);

        // Step 6.
        Console.WriteLine($"Checkout for product 3 and product 7 with the box {box.Id} gave the result \"{checkoutSummary.Result}\"");

        // Step 7. e-mail.
    }
}
