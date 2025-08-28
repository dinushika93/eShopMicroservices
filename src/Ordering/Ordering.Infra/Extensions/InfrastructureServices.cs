using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.App.Data;
using Ordering.Infra.Data;
using Ordering.Infrastructure.Interceptors;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infra.Extensions;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection,IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");

         serviceCollection.AddScoped<IOrderRepository, OrderRepository>();

        serviceCollection.AddScoped<ISaveChangesInterceptor, AuditSaveAsyncInterceptor>();
        serviceCollection.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        serviceCollection.AddDbContext<ApplicationDbContext>((sp, options) =>

        {
            options.UseSqlServer(connectionString);
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
        });

        return serviceCollection;
    }

}