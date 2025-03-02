namespace Basket.Api.Data;

public interface IBasketRepository {

Task<Cart> GetBasket(string userName, CancellationToken cancellationToken);

Task<Cart> SaveBasket(Cart cart, CancellationToken cancellationToken);

Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken);

}
