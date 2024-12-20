﻿namespace Demo.StoreApi.Abstractions;

public record Product
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public decimal Price { get; init; }
}
