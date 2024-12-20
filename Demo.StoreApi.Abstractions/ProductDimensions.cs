namespace Demo.StoreApi.Abstractions;


public record ProductDimensions
{
    public int ProductId { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}
