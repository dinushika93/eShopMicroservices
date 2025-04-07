namespace Ordering.Domain.Models;

public class Order
{
  public Guid Id { get; set; }
  private readonly List<OrderItem> _items = new();
  public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
  public Customer Customer { get; private set; } = null!;
  public Address ShippingAddress { get;  private set;} = null!;
  public Payment Payment { get; private set; } = null!;


  public Order(Guid id, Customer customer, Address shippingAddress, Payment payment)
  {
    Id = id;
    Customer = customer;
    ShippingAddress = shippingAddress;
    Payment = payment;
  }

  public void AddItem(Guid productId, int quantity, decimal price)
  {
    var item = new OrderItem(productId, quantity, price);
    _items.Add(item);
  }




} 