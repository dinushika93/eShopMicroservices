using System.Security.Cryptography.X509Certificates;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.features.Basket.DeleteBasket;

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket", async ([FromBody]DeleteBasketCommand request, ISender sender) =>{
            var result = await sender.Send(request);
            
        })
        .WithName("DeleteBasket")
        .ProducesProblem(StatusCodes.Status404NotFound);
    }
}