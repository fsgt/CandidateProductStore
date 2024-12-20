using Demo.Abstractions;
using Demo.StoreApi.Abstractions;

namespace Demo.Store;

internal class CandidateProductStore : ICandidateProductStore
{
    private readonly ICandidateProductStoreApi _candidateProductStoreApi;

    public CandidateProductStore(ICandidateProductStoreApi candidateProductStoreApi)
    {
        _candidateProductStoreApi = candidateProductStoreApi;
    }

    public async Task<Box> CalculateSmallestBoxForTwoProductsAsync(Product product1, Product product2)
    {
        var product1DimensionsTask = _candidateProductStoreApi.GetProductDimensionsAsync(product1.Id);
        var product2DimensionsTask = _candidateProductStoreApi.GetProductDimensionsAsync(product2.Id);
        var boxesTask = _candidateProductStoreApi.GetBoxesAsync();

        await Task.WhenAll(product1DimensionsTask, product2DimensionsTask, boxesTask);

        var dim1 = product1DimensionsTask.Result;
        var dim2 = product2DimensionsTask.Result;

        // 2-dimensional box, aka. a letter.

        // An accurate calculation is not feasable for large amounts of products because the problem is NP-hard.
        // Since we only have two products we can arrange them in every way possible and see which box they fit, testing the smallest boxes first.
        var dimensions = new List<(decimal x, decimal y)>
        {
            (dim1.Height + dim2.Height, Math.Max(dim1.Width, dim2.Width)),
            (dim1.Height + dim2.Width, Math.Max(dim1.Width, dim2.Height)),
            (dim1.Width + dim2.Height, Math.Max(dim1.Height, dim2.Width)),
            (dim1.Width + dim2.Width, Math.Max(dim1.Height, dim2.Height))
        };

        var boxes = boxesTask.Result.ToList();
        boxes.Sort((box1, box2) => (box1.Width * box1.Height).CompareTo(box2.Width * box2.Height));

        foreach (var box in boxes)
        {
            foreach (var dim in dimensions)
            {
                if (dim.x <= box.Width && dim.y <= box.Height)
                    return box;

                if (dim.y <= box.Width && dim.x <= box.Height)
                    return box;
            }
        }

        throw new InvalidOperationException($"The products \"{product1.Id}\" and \"{product2.Id}\" does not fit any of the \"{boxes.Count}\" box(es).");
    }

    public async Task<CheckoutSummary> CheckoutAsync(Box box, Product product1, Product product2)
    {
        return await _candidateProductStoreApi.CheckoutAsync(box.Id, [product1.Id, product2.Id]);
    }

    public async Task<IList<Product>> GetAllProductsAbovePriceAsync(decimal price)
    {
        var allProducts = await _candidateProductStoreApi.GetProductsAsync();
        return allProducts.Where(product => product.Price > price)
            .ToArray();
    }

    public async Task<IList<Product>> GetAllProductsAsync()
    {
        return await _candidateProductStoreApi.GetProductsAsync();
    }
}
