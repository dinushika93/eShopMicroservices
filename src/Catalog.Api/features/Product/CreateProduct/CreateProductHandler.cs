using System.Runtime.CompilerServices;
using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.Repositories;
using Common.Command;
using MediatR;

namespace Catalog.Api.features.Product.CreateProduct
{
public record CreateProductCommand(ProductCreateDto ProductCreateDto) : ICommand<CreateProductResponse>;

public record CreateProductResponse(string Id);

public class CreateProductHandler : ICommandHandler<CreateProductCommand,CreateProductResponse>
{
     private readonly ICatalogRepository _catalogRepository;
     private readonly IMapper _mapper;

    public CreateProductHandler(ICatalogRepository catalogRepository, IMapper mapper)
    {
        _catalogRepository = catalogRepository;
        _mapper = mapper;
    }

    public async Task<CreateProductResponse>  Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
         var product =  _mapper.Map<Models.Product>(request.ProductCreateDto);
         await _catalogRepository.CreateProduct(product);
         return new CreateProductResponse(product.Id);
    }


}
}
