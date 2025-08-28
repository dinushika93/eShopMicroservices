using Ordering.Domain.Interfaces;

namespace Ordering.Domain.Models;

public class Product : Entity<Product>
{

    public ProductId Id { get; private set; } =  default!;

    public string Name { get; private set; } = default!;

    public decimal Price { get; private set; } = default!;

    private Product(ProductId id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
        
    }
    public static Product Create(ProductId productId, string name, decimal price)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        return new Product(productId, name, price);

    }

}