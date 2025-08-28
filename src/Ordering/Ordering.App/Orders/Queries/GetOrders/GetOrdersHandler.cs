
using Common.Pagination;
using Common.Query;
using Ordering.App.Data;
using Ordering.App.Dtos;
using Ordering.App.Extensions;
using Ordering.Domain.Models;

namespace Ordering.App.Orders.Queries.GetOrders;

public record GetOrdersRequest(PaginationRequest paginationRequest) : IQuery<GetOrdersReponse>;

public record GetOrdersReponse(PaginatedResult<OrderDto> Orders);

public class GetOrdersHandler(IOrderRepository orderRepository) : IQueryHandler<GetOrdersRequest, GetOrdersReponse>
{
    public async Task<GetOrdersReponse> Handle(GetOrdersRequest request, CancellationToken cancellationToken)
    {
        var pageSize = request.paginationRequest.PageSize;
        var index = request.paginationRequest.PageIndex;

        var count = await orderRepository.CountOrders();
        var orders = await orderRepository.GetOrders(index, pageSize, cancellationToken);

        return new GetOrdersReponse(new PaginatedResult<OrderDto>
        (
            pageIndex: index,
            pageSize: pageSize,
            count: count,
            data:  orders.MapOrders()
        ));
     
    }
}