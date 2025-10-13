
using System.ComponentModel;
using Common.Query;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Ordering.App.Data;
using Ordering.App.Dtos;
using Ordering.App.Extensions;
using Ordering.Domain.Models;

namespace Ordering.App.Orders.Queries.GetOrderById;

public record GetOrderByIdRequest(Guid id) : IQuery<GetOrderByIdResponse>;

public record GetOrderByIdResponse(OrderDto OrderDto);

public class GetOrderByIdHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrderByIdRequest, GetOrderByIdResponse>
{
    public async Task<GetOrderByIdResponse> Handle(GetOrderByIdRequest request, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(request.id);
        var order = await orderRepository.GetOrder(orderId);
    
        return new GetOrderByIdResponse(order.ToOrderDto());
    }
}