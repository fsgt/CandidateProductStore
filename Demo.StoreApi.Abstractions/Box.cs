namespace Demo.StoreApi.Abstractions;

public record Box
{
    public int Id { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}
