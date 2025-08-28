using System.Windows.Input;
using Ordering.App.Dtos;
using Common.Command;
using Ordering.Domain.Models;
using Ordering.App.Data;


namespace Ordering.App.Orders.Commands.CreateOrder;

 public record CreateOrderCommand(OrderDto order) : ICommand<CreateOrderResponse>;
public record CreateOrderResponse(Guid Id);

public class CreateOrderHandler(IOrderRepository ordererRepository) : ICommandHandler<CreateOrderCommand, CreateOrderResponse>
{
    public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = CreateOrder(request.order);
        await ordererRepository.CreateOrder(order);

        return new CreateOrderResponse(order.Id.Value);
    }

    private Order CreateOrder(OrderDto orderDto)
    {
        var shippingAddressDto = orderDto.ShippingAddress;
        var shippingAddress = Address.Of(shippingAddressDto.FirstName, shippingAddressDto.LastName,
            shippingAddressDto.EmailAddress, shippingAddressDto.AddressLine, shippingAddressDto.Country,
            shippingAddressDto.State, shippingAddressDto.ZipCode);

        var billingAddressDto = orderDto.BillingAddress;
        var billingAddress = Address.Of(billingAddressDto.FirstName, billingAddressDto.LastName,
            billingAddressDto.EmailAddress, billingAddressDto.AddressLine, billingAddressDto.Country,
            billingAddressDto.State, billingAddressDto.ZipCode);

        var paymentDto = orderDto.Payment;
        var payment = Payment.Of(paymentDto.CardName, paymentDto.CardNumber, paymentDto.Expiration,
            paymentDto.Cvv, paymentDto.PaymentMethod);

        var order = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(orderDto.CustomerId),
            shippingAddress,
            billingAddress,
            payment
        );

        orderDto.OrderItems.ForEach(item => order.AddItem(
            ProductId.Of(item.ProductId),
            item.Quantity,
            item.Price
        ));

        return order;
    }
     
}
