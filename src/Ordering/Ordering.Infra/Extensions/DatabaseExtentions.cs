using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infra.Data;

namespace Ordering.Infrastructure.Data.Extensions;

public static class DatabaseExtentions
{
    public static async Task InitialiseDatabaseAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCustomers(context);
        await SeedProducts(context);
        await SeedOrdersWithItems(context);

        await context.SaveChangesAsync();


    }

    private static async Task SeedCustomers(ApplicationDbContext context)
    {
        if (!await context.Customers.AnyAsync())
        {
            context.Customers.AddRange(InitialData.Customers);
        }
      
    }

    private static async Task SeedProducts(ApplicationDbContext context)
    {
        if (!await context.Products.AnyAsync())
        {
            context.Products.AddRange(InitialData.Products);
        }
    }


    private static async Task SeedOrdersWithItems(ApplicationDbContext context)
    {
        if (!await context.Orders.AnyAsync())
        {
            context.Orders.AddRange(InitialData.Orders);
        }

    }
}