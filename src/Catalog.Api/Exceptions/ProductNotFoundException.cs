using Common.Exceptions;

namespace Catalog.Api.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public  ProductNotFoundException(string property) : base("product", property)
    {


    }
    
} 