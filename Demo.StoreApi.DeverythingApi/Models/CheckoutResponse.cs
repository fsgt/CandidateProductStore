namespace Demo.StoreApi.DeverythingApi.Models;

internal record CheckoutResponse
{
    public int StatusCode { get; init; }
    public string? Result { get; init; }
}
