using Basket.Api.Data;
using Basket.Api.Dtos;
using Common.Command;
using Mapster;
using MassTransit;
using Messaging.Events;

namespace Basket.Api.features.Basket.CheckoutBasket;


public record CheckoutBasketCommand(CheckoutBasketDto CheckoutBasketDto) : ICommand<CheckoutBasketResponse>;

public record CheckoutBasketResponse(bool isSuccess);


public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResponse>
{
    public async Task<CheckoutBasketResponse> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
    {
        var basket = await basketRepository.GetBasket(request.CheckoutBasketDto.UserName, cancellationToken);

        if (basket == null)
            return new CheckoutBasketResponse(false);

        var checkoutBasketEvent = request.CheckoutBasketDto.Adapt<BasketCheckoutEvent>();
        checkoutBasketEvent.TotalPrice = basket.Total;

        await publishEndpoint.Publish(checkoutBasketEvent);

        await basketRepository.DeleteBasket(request.CheckoutBasketDto.UserName, cancellationToken);

        return new CheckoutBasketResponse(true);        
    }
}

