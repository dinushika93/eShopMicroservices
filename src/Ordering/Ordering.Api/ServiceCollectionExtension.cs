using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Api;

public static class ServiceCollectionExtension
{
 public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection)
 {
    return serviceCollection;
 }
}