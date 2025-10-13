using Basket.Api.Dtos;
using Basket.Api.features.Basket.CheckoutBasket;
using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.features.Basket.CheckoutBasket;
public class CheckoutBasketEndpoint : ICarterModule
{
    public record CheckoutBasketRequest(CheckoutBasketDto CheckoutBasketDto);
    public record CheckoutBasketResponse(bool IsSuccess);

    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<CheckoutBasketCommand>();
            var result = await sender.Send(command);
            return Results.Ok(result);
        })
        .WithName("CheckoutBasket")
        .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
