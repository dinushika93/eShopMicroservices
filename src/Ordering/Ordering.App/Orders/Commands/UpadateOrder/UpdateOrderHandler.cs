using System.Windows.Input;
using Ordering.App.Dtos;
using Common.Command;
using Ordering.Domain.Models;
using Ordering.App.Data;
using Ordering.App.Exceptions;

namespace Ordering.App.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand(OrderDto order) : ICommand<UpdateOrderResponse>;

public record UpdateOrderResponse(bool IsSuccess);


public class UpdateOrderHandler(IOrderRepository ordereRepository) : ICommandHandler<UpdateOrderCommand, UpdateOrderResponse>
{
    public async Task<UpdateOrderResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.order.Id);
        var order = await ordereRepository.GetOrderById(orderId);
        if (order is null)
        {
            throw new OrderNotFoundException(request.order.Id.ToString());
        }
       
        UpdateOrder(order, request.order);
        var isSuccess = await ordereRepository.UpdateOrder(order);   

        return new UpdateOrderResponse(isSuccess);
    }

    private void UpdateOrder(Order order, OrderDto orderDto)
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

         order.Update(
            shippingAddress,
            billingAddress,
            payment,
            orderDto.Status
        );
    }
}