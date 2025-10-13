using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Common.Behaviors;
using MassTransit;
using Messaging.Extensions;
using Microsoft.FeatureManagement;

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
      serviceCollection.AddFeatureManagement();
      serviceCollection.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

      return serviceCollection;
   }
}