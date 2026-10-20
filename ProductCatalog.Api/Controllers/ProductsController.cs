using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Utils;

namespace ProductCatalog.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
        (IProductsService productsService, ILogger<ProductsController> logger) : ControllerBase {
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedRetrievingAllMessage, EntityNames.Product.PluralLower);
            var products = await productsService.GetAllAsync(cancellationToken);

            logger.LogInformation(Messages.SuccessfullyRetrievedAllMessage, EntityNames.Product.PluralLower);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedRetrievingEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            var product = await productsService.GetByIdAsync(id, cancellationToken);

            if (product == null) {
                logger.LogInformation(Messages.EntityWithIdNotFoundMessage, EntityNames.Product.SingularUpper, id);
                return NotFound(Messages.FormatEntityWithIdNotFoundErrorMessage(EntityNames.Product.SingularUpper, id));
            }

            logger.LogInformation(Messages.SuccessfullyRetrievedEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedCreatingEntityMessage, EntityNames.Product.SingularLower);
            var createdProduct = await productsService.CreateAsync(request, cancellationToken);

            logger.LogInformation(Messages.SuccessfullyCreatedEntityWithIdMessage, EntityNames.Product.SingularLower, createdProduct.Id);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedUpdatingEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            var updatedProduct = await productsService.UpdateAsync(id, request, cancellationToken);

            logger.LogInformation(Messages.SuccessfullyUpdatedEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedDeletingEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            await productsService.DeleteAsync(id, cancellationToken);

            logger.LogInformation(Messages.SuccessfullyDeletedEntityWithIdMessage, EntityNames.Product.SingularLower, id);
            return NoContent();
        }
    }
}
