using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.App.Dtos;
using Ordering.App.Orders.Queries.GetOrderById;

namespace Ordering.Api.EndPoints;

public record GetOrderByIdResponse(OrderDto OrderDto);
public class GetOrderById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrderByIdRequest(id));
            var response = result.Adapt<GetOrderByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrderById")
        .Produces<GetOrderByIdResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Create order");
        ;
    }
}
