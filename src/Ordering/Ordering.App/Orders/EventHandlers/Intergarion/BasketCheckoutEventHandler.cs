using MassTransit;
using MediatR;
using Messaging.Events;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.App.Dtos;
using Ordering.App.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;
using Ordering.Domain.Events;
using Ordering.Domain.Models;

namespace Ordering.App.Orders.EventHandlers;

public class BasketCheckoutEventHandler(ILogger<BasketCheckoutEventHandler> logger, ISender sender) : IConsumer<BasketCheckoutEvent>
{
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        logger.LogInformation("Handling intergration event of type {}", context.Message.GetType());
        var orderDto = MapToOrderDto(context.Message);
        await sender.Send(new CreateOrderCommand(orderDto));
    }
            
    private static OrderDto MapToOrderDto(BasketCheckoutEvent message)
    {
        var addressDto = new AddressDto(
            message.FirstName,
            message.LastName,
            message.EmailAddress,
            message.AddressLine,
            message.Country,
            message.State,
            message.ZipCode
        );

        var paymentDto = new PaymentDto(
            message.CardName,
            message.CardNumber,
            message.Expiration,
            message.CVV,
            message.PaymentMethod
        );

        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto
        (
            orderId,
            message.CustomerId,
            addressDto,
            addressDto,
            paymentDto,
            new List<OrderItemDto>
            {
                new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 2, 500),
                new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
            },
            OrderStatus.Pending
        );

        return orderDto;
    }
}
