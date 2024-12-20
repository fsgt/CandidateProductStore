namespace Demo.StoreApi.Abstractions;

public interface ICandidateProductStoreApi
{
    public Task<IList<Product>> GetProductsAsync();
    public Task<ProductDimensions> GetProductDimensionsAsync(int productId);
    public Task<IList<Box>> GetBoxesAsync();
    public Task<CheckoutSummary> CheckoutAsync(int boxId, int[] productIds);
}
