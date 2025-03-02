using Catalog.Api.Dtos;
using Catalog.Api.Exceptions;
using Catalog.Api.features.Product.CreateProduct;
using Catalog.Api.features.Product.DeleteProduct;
using Catalog.Api.features.Product.GetProductByCategory;
using Catalog.Api.features.Product.GetProductById;
using Catalog.Api.features.Product.GetProducts;
using Catalog.Api.features.Product.UpdateProduct;
using Catalog.Api.Models;
using Catalog.Api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{

    [ApiController]
    [Route("api/v1/products")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMediator _mediator;


        public CatalogController(ICatalogRepository catalogRepository, IMediator mediator)
        {
            _catalogRepository = catalogRepository;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int? pageNumber = 1, int? pageSize = 10)
        {

            var getProductQuery = new GetProductsQuery(pageNumber, pageSize);
            var products = await _mediator.Send(getProductQuery);
            return Ok(products);

        }

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ActionResult<ProductDto>> GetProductById(string id)
        {
            var getProductByIdQuery = new GetProductByIdQuery(id);
            var result =  await _mediator.Send(getProductByIdQuery);
            if (result.product == null)
            {
                 throw new ProductNotFoundException(id);
            }
            return Ok(result);
        }

        [HttpGet("[action]/{name}", Name = "GetProductByName")]
        public async Task<ActionResult<Product>> GetProductByName(string name)
        {
            var product = await _catalogRepository.GetProductByName(name);
            if (product == null)
            {
               throw new ProductNotFoundException(name);
            }
            return Ok(product);
        }

        [HttpGet("[action]/{category}", Name = "GetProductByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductByCategory(string category)
        {
            var result = await _mediator.Send(new GetProductsByCategoryQuery(category));
            if (result.products == null)
            {
                return NotFound($"Products does not exist with category {category}");
            }
            return Ok(result);

        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto){
            var createProductCommand = new CreateProductCommand(productCreateDto);
            var result =  await _mediator.Send(createProductCommand);
            return CreatedAtRoute(nameof(GetProductById), new {id = result.Id},  productCreateDto);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductDto product){
            var result = await _mediator.Send(new UpdateProductCommand(product));
            if( result.isSuccess){
                return Ok(result);
            }
            else return BadRequest();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id){
            var isSuccess = await _mediator.Send(new DeleteProductCommand(id));
            if(isSuccess){
                return Ok(isSuccess);
            }
            else return BadRequest();
        }
    }

}
