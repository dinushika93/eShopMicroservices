



using Carter;
using Common.Pagination;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.App.Dtos;
using Ordering.App.Orders.Queries.GetOrderById;
using Ordering.App.Orders.Queries.GetOrders;

namespace Ordering.Api.EndPoints;

public record GetOrdersReponse(PaginatedResult<OrderDto> Orders);

public class GetOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
        var result = await sender.Send(new GetOrdersRequest(request));

        var response = result.Adapt<GetOrdersReponse>();

        return Results.Ok(response);
        })
    .WithName("GetOrders")
    .Produces<GetOrdersReponse>(StatusCodes.Status200OK)
    .ProducesProblem(StatusCodes.Status400BadRequest)
    .ProducesProblem(StatusCodes.Status404NotFound)
    .WithSummary("Get Orders")
    .WithDescription("Get Orders");
    }
}