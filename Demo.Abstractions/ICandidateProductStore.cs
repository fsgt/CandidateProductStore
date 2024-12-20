using Demo.StoreApi.Abstractions;

namespace Demo.Abstractions;

public interface ICandidateProductStore
{
    public Task<IList<Product>> GetAllProductsAsync();
    public Task<IList<Product>> GetAllProductsAbovePriceAsync(decimal price);
    public Task<Box> CalculateSmallestBoxForTwoProductsAsync(Product product1, Product product2);
    public Task<CheckoutSummary> CheckoutAsync(Box box, Product product1, Product product2);
}
