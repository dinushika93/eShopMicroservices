
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
        var orders = await orderRepository.GetOrdersByCustomer(customerId, cancellationToken);

        return new GetOrdersByCustomerResponse(orders.ToOrderDtoList());
    }
}