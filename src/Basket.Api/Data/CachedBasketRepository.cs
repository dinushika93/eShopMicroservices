using System.Text.Json;
using Basket.Api.Data;
using Basket.Api.Exceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Api.Data;

public class CachedBasketRepository(IBasketRepository repo, IDistributedCache cache) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        await repo.DeleteBasket(userName, cancellationToken);
        await cache.RemoveAsync(userName,cancellationToken);

        return true;
     
    }

    public async Task<Cart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var basketCached = await cache.GetStringAsync(userName, cancellationToken);
        if(!string.IsNullOrEmpty(basketCached)){
          return JsonSerializer.Deserialize<Cart>(basketCached);
        }
        
       var basket =await  repo.GetBasket(userName, cancellationToken);
       await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

       return basket;
    }

    public async Task<Cart> SaveBasket(Cart cart, CancellationToken cancellationToken)
    {
        var basket = await repo.SaveBasket(cart,cancellationToken);
        await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }
}