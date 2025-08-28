using Carter;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using Ordering.App.Dtos;
using Ordering.App.Orders.Commands.CreateOrder;
using Ordering.App.Orders.Commands.DeleteOrder;

namespace Ordering.Api.EndPoints;

public record DeleteOrderResponse(bool isSuccess);

public class DeleteOrder() : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteOrderCommand(id));
            var response = result.Adapt<DeleteOrderResponse>();

            return Results.Ok();
        })
        .WithName("DeleteOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Delete order");
    }
}