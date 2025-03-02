using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.Repositories;
using Common.Query;

namespace Catalog.Api.features.Product.GetProductByCategory;

public record GetProductsByCategoryQuery(string category) : IQuery<GetProductByCategoryResponse>;

public record GetProductByCategoryResponse(IEnumerable<ProductDto> products);

internal class GetProductByCategoryHandler(ICatalogRepository repo, IMapper mapper, ILogger<GetProductByCategoryHandler> logger) : IQueryHandler<GetProductsByCategoryQuery, GetProductByCategoryResponse>
{
    public async Task<GetProductByCategoryResponse> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
       logger.LogInformation($"Getting the product by category {request.category}");
       var products = await repo.GetProductByCategory(request.category);
       return new GetProductByCategoryResponse(mapper.Map<IEnumerable<ProductDto>>(products));
       
    }
}