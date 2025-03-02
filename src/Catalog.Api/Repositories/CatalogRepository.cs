using System.Runtime.CompilerServices;
using Catalog.Api.Data;
using Catalog.Api.Models;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogdbContext _catalogDbContext;
        public CatalogRepository(CatalogdbContext catalogDbContext)
        {
            _catalogDbContext = catalogDbContext;
        }

        public async Task CreateProduct(Product product)
        {
           await _catalogDbContext.products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, id);

            var result = await _catalogDbContext.products.DeleteOneAsync(filter);
            return  result.DeletedCount > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, id);
            return await _catalogDbContext.products.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Category, category);
            return await _catalogDbContext.products.Find(filter).ToListAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Name, name);
            return await _catalogDbContext.products.Find(filter).FirstOrDefaultAsync();
        }

        public  async Task<IEnumerable<Product>> GetProducts(int? pageNumber, int? pageSize)
        {
            var filter = Builders<Product>.Filter.Empty;
            var result = await _catalogDbContext.products.Find(filter)
                            .ToListAsync();
            return result.Skip(pageNumber ?? 0 * pageSize ?? 0).Take(pageSize ?? 0).ToList();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, product.Id);
            var result = await _catalogDbContext.products.ReplaceOneAsync(filter, product);
            return result.IsAcknowledged;


        }


    }
}



