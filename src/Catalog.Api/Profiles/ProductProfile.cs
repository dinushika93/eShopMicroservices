using AutoMapper;
using Catalog.Api.Dtos;
using Catalog.Api.features.Product.CreateProduct;
using Catalog.Api.Models;
using MongoDB.Bson;

namespace Catalog.Api.Profiles;
public class ProductProfile : Profile{
    public ProductProfile(){
        CreateMap<Product, ProductCreateDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ObjectId, string>().ConvertUsing(o => o.ToString());
    }
}