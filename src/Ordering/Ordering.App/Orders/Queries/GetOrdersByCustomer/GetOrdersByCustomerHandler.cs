
using System.ComponentModel;
using Common.Query;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Ordering.App.Data;
using Ordering.App.Dtos;
using Ordering.App.Exceptions;
using Ordering.App.Extensions;
using Ordering.Domain.Models;

namespace Ordering.App.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerRequest(Guid customerId) : IQuery<GetOrdersByCustomerResponse>;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> OrderDto);

public class GetOrderByCustomerHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrdersByCustomerRequest, GetOrdersByCustomerResponse>
{
    public async Task<GetOrdersByCustomerResponse> Handle(GetOrdersByCustomerRequest request, CancellationToken cancellationToken)
    {
        var customerId = CustomerId.Of(request.customerId);
        var orders = await orderRepository.GetOrdersByCustomer(customerId);

        return new GetOrdersByCustomerResponse(orders.MapOrders());
    }

    // private IEnumerable<OrderDto> MapOrder(IEnumerable<Order> orders)
    // {
    //     return orders.Select(order => new OrderDto
    //     (
    //         order.Id.Value,
    //         order.CustomerId.Value,
    //         new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress,
    //             order.BillingAddress.AddressLine, order.BillingAddress.Country,
    //             order.BillingAddress.State, order.BillingAddress.ZipCode),
    //         new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress,
    //             order.ShippingAddress.AddressLine, order.ShippingAddress.Country,
    //             order.ShippingAddress.State, order.ShippingAddress.ZipCode),
    //         new PaymentDto(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration,
    //              order.Payment.CVV, order.Payment.PaymentMethod),
    //              order.Items.Select(item => new OrderItemDto(item.Id.Value, item.OrderId.Value, item.ProductId.Value, item.Quantity, item.Price)).ToList(),
    //         order.Status
    //     ));
    // }
}