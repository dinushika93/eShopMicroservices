using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.Repositories;
using Common.Query;

namespace Catalog.Api.features.Product.GetProductById;

public record GetProductByIdQuery(string Id) :  IQuery<GetProductByIdResponse>;

public record GetProductByIdResponse(ProductDto product);

public class GetProductByIdHandler(ICatalogRepository repo, IMapper mapper, ILogger<GetProductByIdHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
{
    public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
      logger.LogInformation($"Getting the product by Id {request.Id}");
      var product  = await repo.GetProduct(request.Id);
      return new GetProductByIdResponse(mapper.Map<ProductDto>(product));

    }
}

