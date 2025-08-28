using Ordering.Domain.Models;

namespace Ordering.App.Data;

public interface IOrderRepository
{
    Task CreateOrder(Order order);
    Task<Order> GetOrderById(OrderId orderId);
    Task<bool> UpdateOrder(Order order);
    Task<bool> DeleteOrder(Order order);
    Task<Order> GetOrderWithItems(OrderId orderId);
    Task<IEnumerable<Order>> GetOrdersByCustomer(CustomerId customerId);
    Task<IEnumerable<Order>> GetOrders(int index, int pageSize, CancellationToken cancellation);
    Task<long> CountOrders();

}
