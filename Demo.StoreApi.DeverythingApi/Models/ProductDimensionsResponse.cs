namespace Demo.StoreApi.DeverythingApi.Models;

internal record ProductDimensionsResponse
{
    public int StatusCode { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}
