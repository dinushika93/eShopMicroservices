using Ordering.App.Data;
using Ordering.Domain.Models;
using Ordering.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
{
    public Task CreateOrder(Order order)
    {
        dbContext.Orders.Add(order);
        return dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteOrder(Order order)
    {
        dbContext.Orders.Remove(order);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Order> GetOrderById(OrderId orderId)
    {
        return await dbContext.Orders.FindAsync(orderId);
    }

    public async Task<Order> GetOrderWithItems(OrderId orderId)
    {
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id== orderId);
        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomer(CustomerId customerId)
    {
        var order = dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId);

        return order;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        dbContext.Orders.Update(order);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Order>> GetOrders(int index, int pageSize, CancellationToken cancellationToken)
    {
        return await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Skip(index * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> CountOrders()
    {
        return await dbContext.Orders.LongCountAsync();
    }

}

