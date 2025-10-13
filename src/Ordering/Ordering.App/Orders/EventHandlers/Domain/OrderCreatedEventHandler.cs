using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement;
using Ordering.App.Extensions;
using Ordering.Domain.Events;
using Ordering.Domain.Models;

namespace Ordering.App.Orders.EventHandlers;

public class OrderCreatedEventHandler(IFeatureManager featureManager, IPublishEndpoint publishEndpoint, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling domain event of type {}", notification.GetType());
        if (await featureManager.IsEnabledAsync("OrderFullfillment"))
        {
            var orderDto = notification.order.ToOrderDto();
            // Piuvlish OrderCreated Intergration Event
            await publishEndpoint.Publish(orderDto, cancellationToken);
        }
    }
}