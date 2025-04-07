namespace Ordering.Domain.Models;

public class OrderItem
{
    public Guid Id { get; private set; } = default!;
    public Guid  ProductId { get; private set; } = default!;
    public int Quantity { get; private set; } = default!;
    public decimal Price { get; private set; } = default!;

    public OrderItem(Guid productId, int quantity, decimal price)
    {
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }
}
