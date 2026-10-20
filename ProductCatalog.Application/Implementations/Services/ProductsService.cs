using ProductCatalog.Application.Abstractions.Repositories;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.Mapping;
using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Utils;

namespace ProductCatalog.Application.Implementations.Services {
    public class ProductsService(IProductsRepository productsRepository) : IProductsService {
        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken) {
            var products = await productsRepository.GetAllAsync(cancellationToken);

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken) {
            var product = await productsRepository.GetByIdAsync(id, cancellationToken);

            return product;
        }

        public async Task<Product> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken) {
            request.Validate();
            
            var productToCreate = ProductMapper.ToEntity(request);

            var createdProduct = await productsRepository.AddAsync(productToCreate, cancellationToken);
            await productsRepository.SaveChangesAsync(cancellationToken);

            return createdProduct;
        }

        public async Task<Product> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken) {
            request.Validate();

            var productToUpdate = await productsRepository.GetByIdAsync(id, cancellationToken)
               ?? throw new EntityNotFoundException(
                    Messages.FormatEntityWithIdNotFoundErrorMessage(EntityNames.Product.SingularUpper, id)
               );

            ProductMapper.Apply(productToUpdate, request);

            productsRepository.Update(productToUpdate);
            await productsRepository.SaveChangesAsync(cancellationToken);

            return productToUpdate;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken) {
            var productToDelete = await productsRepository.GetByIdAsync(id, cancellationToken)
               ?? throw new EntityNotFoundException(
                    Messages.FormatEntityWithIdNotFoundErrorMessage(EntityNames.Product.SingularUpper, id)
               );

            productsRepository.Delete(productToDelete);
            await productsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
