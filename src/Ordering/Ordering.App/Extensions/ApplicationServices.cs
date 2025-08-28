using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Common.Behaviors;

namespace Ordering.App.Extensions;

public static class ApplicationServices
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
   {
      serviceCollection.AddMediatR(config =>
      {
         config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
         config.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
         config.AddOpenBehavior(typeof(LoggingBehavior<,>));
      });

      return serviceCollection;
   }
}