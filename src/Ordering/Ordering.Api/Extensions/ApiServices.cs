using Carter;

namespace Ordering.Api.Extensions;

public static class ApiServices
{
   public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection, ConfigurationManager configuration)
   {
      serviceCollection.AddCarter();
      return serviceCollection;
   }

   public static WebApplication UseApiServices(this WebApplication webApplication)
   {
      webApplication.MapCarter();
      return webApplication;
   }
}