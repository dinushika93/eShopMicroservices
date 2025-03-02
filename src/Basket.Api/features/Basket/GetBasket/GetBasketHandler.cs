global using Basket.Api.Models;
using Basket.Api.Data;
using Common.Query;
using Microsoft.AspNetCore.DataProtection.Repositories;

namespace Basket.Api.features.Basket.GetBasket;

public record GetBasketQuery(string userName) : IQuery<GetBasketResponse>;

public record GetBasketResponse(Cart cart);

public class GetBasketHandler(IBasketRepository repo) : IQueryHandler<GetBasketQuery, GetBasketResponse>
{
    public async Task<GetBasketResponse> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await repo.GetBasket(request.userName, cancellationToken);
        return new GetBasketResponse(basket);
    }

}
