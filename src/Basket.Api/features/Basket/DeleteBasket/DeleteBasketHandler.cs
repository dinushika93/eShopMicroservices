using Basket.Api.Data;
using Common.Command;

namespace Basket.Api.features.Basket.DeleteBasket;

public record DeleteBasketCommand(string userName) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool isSuccess);

public class DeleteBasketHandler(IBasketRepository repo) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var isSuccess = await repo.DeleteBasket(request.userName, cancellationToken);
        return new DeleteBasketResult(isSuccess);
    }
}