using Carter;
using Mapster;
using MediatR;
using Ordering.App.Dtos;
using Ordering.App.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.Api.EndPoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> OrderDto);
public class GetOrdersByCustomer : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/order/customer/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetOrdersByCustomerRequest(id));
            var response = result.Adapt<GetOrdersByCustomerResponse>();

            return Results.Ok(response);
        })
        .WithName("GetOrderByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithDescription("Get order by customer");
    }
}
