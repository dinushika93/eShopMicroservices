using System.Runtime.CompilerServices;
using Catalog.Api.Data;
using Catalog.Api.Models;
using MongoDB.Driver;

namespace Catalog.Api.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly CatalogdbContext _catalogDbContextDbContext;
        public CatalogRepository(CatalogdbContext catalogDbContextDbContext)
        {
            _catalogDbContextDbContext = catalogDbContextDbContext;
        }

        public async Task CreateProduct(Product product)
        {
           await _catalogDbContextDbContext.products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, id);

            var result = await _catalogDbContextDbContext.products.DeleteOneAsync(filter);
            return  result.IsAcknowledged;
        }

        public async Task<Product> GetProduct(string id)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, id);
            return await _catalogDbContextDbContext.products.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Product> GetProductByCategory(string category)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Category, category);
            return await _catalogDbContextDbContext.products.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Name, name);
            return await _catalogDbContextDbContext.products.Find(filter).FirstOrDefaultAsync();
        }

        public  async Task<IEnumerable<Product>> GetProducts()
        {
            var filter = Builders<Product>.Filter.Empty;
            return await _catalogDbContextDbContext.products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var filter = Builders<Product>.Filter.Eq(p=>p.Id, product.Id);
            var result = await _catalogDbContextDbContext.products.ReplaceOneAsync(filter, product);
            return result.IsAcknowledged;


        }


    }
}



