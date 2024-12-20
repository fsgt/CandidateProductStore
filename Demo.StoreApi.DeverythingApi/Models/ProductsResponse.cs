﻿namespace Demo.StoreApi.DeverythingApi.Models;
internal record ProductsResponse
{
    internal record Product
    {
        public int Id { get; init; }
        public string? Name { get; init; }
        public decimal Price { get; init; }
    }

    public int StatusCode { get; init; }
    public Product[]? Products { get; init; }
}
