using Basket.Api.Data;
using Common.Command;
using Discount.Api;

namespace Basket.Api.features.Basket.SaveBasket;

public record SaveBasketCommand(Cart cart) : ICommand<SaveBasketResult>;

public record SaveBasketResult(string userName);

public class SaveBasketHandler(
    IBasketRepository repo, 
    //DiscountService.DiscountServiceClient discountServiceClient, 
    ILoggerFactory loggerFactory) 
    : ICommandHandler<SaveBasketCommand, SaveBasketResult>
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveBasketHandler>();

    public async Task<SaveBasketResult> Handle(SaveBasketCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Im here inside Handle");
        //CalculateDiscount(discountServiceClient, request.cart);
        var cart = await repo.SaveBasket(request.cart, cancellationToken);
        return new SaveBasketResult(cart.UserName);
    }

    // private void CalculateDiscount(DiscountService.DiscountServiceClient discountServiceClient, Cart cart)
    // {
    //     _logger.LogInformation("Im here at CalculateDiscount");
    //     foreach (var item in cart.Items)
    //     {
    //         var discount = discountServiceClient.GetDiscount(new GetDiscountRequest { ProductName = item.ProductName });
    //         item.Price -= discount.Amount;
    //     }
    // }
}
