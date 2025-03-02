using System.Windows.Input;
using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.Repositories;
using Common.Command;

namespace Catalog.Api.features.Product.UpdateProduct;

public record UpdateProductCommand(ProductDto ProductDto) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(bool isSuccess);

internal class UpdateProductHandler(ICatalogRepository repo, IMapper mapper, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResponse>
{
    public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating the product with Id : {request.ProductDto.Id} ");

        var product = repo.GetProduct(request.ProductDto.Id);
        if(product == null)
        {
              //Exception

        }    
        var isSuccess = await repo.UpdateProduct(mapper.Map<Models.Product>(request.ProductDto));
        return new UpdateProductResponse(isSuccess);
    }
}