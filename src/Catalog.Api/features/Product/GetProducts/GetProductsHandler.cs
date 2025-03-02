using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.features.Product.CreateProduct;
using Catalog.Api.Repositories;
using Common.Query;
using DnsClient.Protocol;

namespace Catalog.Api.features.Product.GetProducts;

public record GetProductsQuery(int? pageNumber = 1, int? pageSize = 10) : IQuery<GetProductsResponse>;

public record GetProductsResponse(IEnumerable<ProductDto> Products);

public class GetProductsHandler(ICatalogRepository repo, IMapper mapper) : IQueryHandler<GetProductsQuery, GetProductsResponse>
{
    public async Task<GetProductsResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
       var products  = await repo.GetProducts(request.pageNumber, request.pageSize);
       return new GetProductsResponse(mapper.Map<IEnumerable<Models.Product>, IEnumerable<ProductDto>>(products));
    }
}