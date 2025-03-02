using Carter;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.features.Basket.GetBasket;

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async(string userName, ISender sender)=>{
            var response = await sender.Send(new GetBasketQuery(userName));
            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}