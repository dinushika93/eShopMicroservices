using Catalog.Api.Models;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;


        public CatalogController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {

            var products = await _catalogRepository.GetProducts();
            return Ok(products);

        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<Product>> GetProductById(string id)
        {
            var product = _catalogRepository.GetProduct(id);
            if (product == null)
            {
                return NotFound(product);
            }
            return Ok(product);
        }

        [HttpGet("[action]/{name}", Name = "GetProductByName")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _catalogRepository.GetProductByName(name);
            if (product == null)
            {
                return NotFound(product);
            }
            return product;
        }

        [HttpGet("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<ActionResult<Product>> GetProductByCategory(string category)
        {
            var product = await _catalogRepository.GetProductByCategory(category);
            if (product == null)
            {
                return NotFound(product);
            }
            return product;
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product){
            await _catalogRepository.CreateProduct(product);
            return CreatedAtRoute(nameof(GetProductById), new {id = product.Id}, product);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product){
            if( await _catalogRepository.UpdateProduct(product)){
                return Ok(product);
            }
            else return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id){
            if( await _catalogRepository.DeleteProduct(id)){
                return Ok();
            }
            else return BadRequest();
        }
    }

}
