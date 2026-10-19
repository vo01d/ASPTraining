using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.RequestsAndResponces.Products;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductsService productsService) : ControllerBase {
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            var products = await productsService.GetAllAsync(cancellationToken);

            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) {
            var product = await productsService.GetByIdAsync(id, cancellationToken);

            if (product == null) {
                return NotFound($"Product with Id={id} was not found.");
            }

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateOrUpdateProductRequest request, CancellationToken cancellationToken) {
            var createdProduct = await productsService.CreateAsync(request, cancellationToken);

            return CreatedAtAction(nameof(Create), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, [FromBody] CreateOrUpdateProductRequest request, CancellationToken cancellationToken) {
            var updatedProduct = await productsService.UpdateAsync(id, request, cancellationToken);
            
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) {
            await productsService.DeleteAsync(id, cancellationToken);
            
            return NoContent();
        }
    }
}
