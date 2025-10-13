using Ordering.App.Dtos;
using Ordering.Domain.Models;


namespace Ordering.App.Extensions;

public static class OrderExtension
{
    public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
    {
        return orders.Select(order => order.MapOrder());
    }
    

    public static OrderDto ToOrderDto(this Order order) => order.MapOrder();

    private static OrderDto MapOrder(this Order order)
    {
        var billingAddress = new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress,
        order.BillingAddress.AddressLine, order.BillingAddress.Country,
        order.BillingAddress.State, order.BillingAddress.ZipCode);

        var shippingAddress = new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress,
        order.ShippingAddress.AddressLine, order.ShippingAddress.Country,
        order.ShippingAddress.State, order.ShippingAddress.ZipCode);

        var payment = new PaymentDto(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration,
        order.Payment.CVV, order.Payment.PaymentMethod);

        return new OrderDto
        (
            order.Id.Value,
            order.CustomerId.Value,
            billingAddress,
            shippingAddress,
             payment,
            order.OrderItems.Select(item => new OrderItemDto(item.OrderId.Value, item.ProductId.Value, item.Quantity, item.Price)).ToList(),
            order.Status
        );
    }
}
