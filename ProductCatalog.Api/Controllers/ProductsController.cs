using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.RequestsAndResponces.Products;
using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Utils;

namespace ProductCatalog.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController
        (IProductsService productsService, ILogger<ProductsController> logger) : ControllerBase 
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedRetrievingAllLogMessage, EntityNames.Product.PluralLower);
                var products = await productsService.GetAllAsync(cancellationToken);

                logger.LogInformation(Messages.SuccessfullyRetrievedAllLogMessage, EntityNames.Product.PluralLower);
                return Ok(products);
            }
            catch (Exception ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredRetrievingAllLogMessage, EntityNames.Product.PluralLower, ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.UnexpectedErrorMessage);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedRetrievingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                var product = await productsService.GetByIdAsync(id, cancellationToken);

                if (product == null) {
                    logger.LogError(Messages.EntityWithIdNotFoundLogMessage, EntityNames.Product.SingularUpper, id);
                    return NotFound(Messages.FormatEntityWithIdNotFoundErrorMessage(EntityNames.Product.SingularUpper, id));
                }

                logger.LogInformation(Messages.SuccessfullyRetrievedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                return Ok(product);
            }
            catch (Exception ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredRetrievingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id, ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.UnexpectedErrorMessage);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken) {
            try {
                request.Validate();
                logger.LogInformation(Messages.StartedCreatingEntityLogMessage, EntityNames.Product.SingularLower);
                var createdProduct = await productsService.CreateAsync(request, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyCreatedEntityWithIdLogMessage, EntityNames.Product.SingularLower, createdProduct.Id);
                return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
            }
            catch (RequestValidationException ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredCreatingEntityLogMessage, EntityNames.Product.SingularLower, ex.Message
                );
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredCreatingEntityLogMessage, EntityNames.Product.SingularLower, ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.UnexpectedErrorMessage);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken) {
            try {
                request.Validate();
                logger.LogInformation(Messages.StartedUpdatingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                var updatedProduct = await productsService.UpdateAsync(id, request, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyUpdatedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                return Ok(updatedProduct);
            }
            catch (Exception ex) when (ex is RequestValidationException || ex is InvalidOperationException) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredUpdatingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id, ex.Message
                );
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredUpdatingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id, ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.UnexpectedErrorMessage);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken) {
            try {
                logger.LogInformation(Messages.StartedDeletingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                await productsService.DeleteAsync(id, cancellationToken);

                logger.LogInformation(Messages.SuccessfullyDeletedEntityWithIdLogMessage, EntityNames.Product.SingularLower, id);
                return NoContent();
            }
            catch (InvalidOperationException ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredDeletingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id, ex.Message
                );
                return BadRequest(ex.Message);
            }
            catch (Exception ex) {
                logger.LogError(
                    ex, 
                    Messages.UnexpectedErrorOccurredDeletingEntityWithIdLogMessage, EntityNames.Product.SingularLower, id, ex.Message
                );
                return StatusCode(StatusCodes.Status500InternalServerError, Messages.UnexpectedErrorMessage);
            }
        }
    }
}
