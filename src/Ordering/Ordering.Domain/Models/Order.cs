using Ordering.Domain.Enums;
using Ordering.Domain.Events;


namespace Ordering.Domain.Models;

public class Order : Entity<Order>
{
  public OrderId Id { get; private set; }
  private readonly List<OrderItem> _orderItems = new();
  public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

  public CustomerId CustomerId { get; private set; } = default!;
  public Address ShippingAddress { get; private set; } = default!;
  public Address BillingAddress { get; private set; } = default!;
  public Payment Payment { get; private set; } = default!;
  public OrderStatus Status { get; private set; } = OrderStatus.Pending;
  public decimal OrderTotal { get => OrderItems.Sum(x => x.Price * x.Quantity); private set { } }


  public static Order Create(OrderId id, CustomerId customerId, Address shippingAddress, Address billingAddress, Payment payment)
  {
    var order = new Order
    {
      Id = id,
      CustomerId = customerId,
      ShippingAddress = shippingAddress,
      BillingAddress = billingAddress,
      Payment = payment,
      Status = OrderStatus.Pending
    };
    order.AddDomainEvents(new OrderCreatedEvent(order));

    return order;
  }

  public void Update(Address shippingAddress, Address billingAddress, Payment payment, OrderStatus status)
  {
    ShippingAddress = shippingAddress;
    BillingAddress = billingAddress;
    Payment = payment;
    Status = status;

    AddDomainEvents(new OrderUpdatedEvent(this));
  }

  public void AddItem(ProductId productId, int quantity, decimal price)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

    var orderItem = new OrderItem(Id, productId, quantity, price);
    _orderItems.Add(orderItem);
  }

  public void RemoveItem(ProductId productId)
  {
    var item = _orderItems.FirstOrDefault(i => i.ProductId == productId);
    if (item != null)
    {
      _orderItems.Remove(item);
    }
  }

}
