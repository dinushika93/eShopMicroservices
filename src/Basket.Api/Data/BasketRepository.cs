using Basket.Api.Data;
using Basket.Api.Exceptions;
using Marten;
using Marten.Internal.Sessions;

namespace Basket.Api.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        session.Delete<Cart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return true;

    }

    public async Task<Cart> GetBasket(string userName, CancellationToken cancellationToken)
    {
       var cart = await session.LoadAsync<Cart>(userName, cancellationToken);
       return cart ?? throw new BasketNotFoundException(userName);
    }

    public async Task<Cart> SaveBasket(Cart cart, CancellationToken cancellationToken)
    {
        session.Store<Cart>(cart);
        await session.SaveChangesAsync(cancellationToken);
        return cart;
    }
}