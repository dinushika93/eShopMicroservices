using Catalog.Api.Models;

namespace Catalog.Api.Repositories
{
    public interface ICatalogRepository
    {
        Task<Product> GetProduct(string id);
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByCategory(string category);
        Task CreateProduct(Product product);
        Task<bool> DeleteProduct(string id);
        Task<bool> UpdateProduct(Product product);
    }

}
