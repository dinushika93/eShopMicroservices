using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.features.Basket.SaveBasket;
public class SaveBasketEndpoint : ICarterModule
{
    public record StoreBasketRequest(Cart Cart);
    public record StoreBasketResponse(string UserName);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async ([FromBody]StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<SaveBasketCommand>();
            var result = await sender.Send(command);
            return Results.Created($"/basket/{result.userName}", result);
        })
        .WithName("SaveBasket")
        .ProducesProblem(StatusCodes.Status400BadRequest)
        ;
    }
}
