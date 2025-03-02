
using System.Windows.Input;
using Amazon.Runtime.Internal;
using AutoMapper;
using Catalog.Api.features.Product.GetProductById;
using Catalog.Api.Repositories;
using Common.Command;

namespace Catalog.Api.features.Product.DeleteProduct;


public record DeleteProductCommand(string id) : ICommand<bool>;



internal class DeleteProductHandler(ICatalogRepository repo, IMapper mapper, ILogger<GetProductByIdHandler> logger) : ICommandHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting the product with Id {request.id}");

        bool isSuccess = await repo.DeleteProduct(request.id);
        return isSuccess;
    }
}