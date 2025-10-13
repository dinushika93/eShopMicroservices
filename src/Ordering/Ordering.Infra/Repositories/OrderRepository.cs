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
        try
        {
            dbContext.Orders.Add(order);
            return dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            throw new Exception("An error occurred while creating the order.", ex);
        }
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

    public async Task<Order> GetOrder(OrderId orderId)
    {
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderId);

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomer(CustomerId customerId, CancellationToken cancellationToken)
    {
        var order = await dbContext.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync(cancellationToken);

        return order;
    }

    public async Task<bool> UpdateOrder(Order order)
    {
        dbContext.Orders.Update(order);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Order>> GetOrders(int index, int pageSize, CancellationToken cancellationToken)
    {

        var orders = await dbContext.Orders
         .Include(o => o.OrderItems)
         .Skip(pageSize * index)
         .Take(pageSize)
         .ToListAsync(cancellationToken);

        return orders;
    }

    public async Task<long> CountOrders()
    {
        return await dbContext.Orders.LongCountAsync();
    }

}

