namespace Demo.StoreApi.DeverythingApi.Models;

internal record BoxesResponse
{
    internal record Box
    {
        public int Id { get; init; }
        public decimal Width { get; init; }
        public decimal Height { get; init; }
    }

    public int StatusCode { get; init; }
    public Box[]? Boxes { get; init; }
}
