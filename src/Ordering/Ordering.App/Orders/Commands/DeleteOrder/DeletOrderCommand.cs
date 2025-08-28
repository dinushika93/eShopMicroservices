using System.Windows.Input;
using Ordering.App.Dtos;
using Common.Command;
using Ordering.Domain.Models;
using Ordering.App.Data;
using Ordering.App.Exceptions;

namespace Ordering.App.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResponse>;

public record DeleteOrderResponse(bool IsSuccess);

public class DeleteOrderHandler(IOrderRepository orderRepository) : ICommandHandler<DeleteOrderCommand, DeleteOrderResponse>
{
    public async Task<DeleteOrderResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
          var orderId = OrderId.Of(request.OrderId);
        var order = await orderRepository.GetOrderById(orderId);
        if (order is null)
        {
            throw new OrderNotFoundException(request.OrderId.ToString());
        }
        var isSuccess = await orderRepository.DeleteOrder(order);

        return new DeleteOrderResponse(isSuccess);
    }
}
