using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Exceptions;
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
            logger.LogInformation(Messages.StartedRetrievingAllLogMessage, EntityNames.Product.PluralLower);
            var products = await productsService.GetAllAsync(cancellationToken);

            logger.LogInformation(Messages.SuccessfullyRetrievedAllLogMessage, EntityNames.Product.PluralLower);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) {
            logger.LogInformation(Messages.StartedRetrievingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
            var product = await productsService.GetByIdAsync(id, cancellationToken);

            if (product == null) {
                logger.LogInformation(Messages.EntityWithIdNotFoundLogMessage, EntityNames.Product.SingularUpper, id);
                return NotFound(Messages.FormatEntityWithIdNotFoundErrorMessage(EntityNames.Product.SingularUpper, id));
            }

            logger.LogInformation(Messages.SuccessfullyRetrievedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedCreatingEntityLogMessage, EntityNames.Product.SingularLower);
                var createdProduct = await productsService.CreateAsync(request, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyCreatedEntityWithIdLogMessage, EntityNames.Product.SingularLower, createdProduct.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (RequestValidationException ex) {
                logger.LogError(ex, Messages.EntityCreationRequestFailedValidationLogMessage, EntityNames.Product.SingularUpper, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedUpdatingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                var updatedProduct = await productsService.UpdateAsync(id, request, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyUpdatedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                return Ok(updatedProduct);
            }
            catch (RequestValidationException ex) {
                logger.LogError(ex, Messages.EntityUpdatingRequestFailedValidationLogMessage, EntityNames.Product.SingularUpper, ex.Message);
                return BadRequest(ex.Message);
            }
            catch (EntityNotFoundException ex) {
                logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedDeletingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                await productsService.DeleteAsync(id, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyDeletedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                logger.LogInformation(ex, ex.Message);
                return NotFound(ex.Message);
            }
        }
    }
}
