using ProductCatalog.Application.Abstractions.Repositories;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.RequestsAndResponces.Products;
using ProductCatalog.Core.Entities;

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

        public async Task<Product> CreateAsync(CreateOrUpdateProductRequest request, CancellationToken cancellationToken) {
            var productToCreate = CreateOrUpdateProductRequest.ToDbModel(request);

            var createdProduct = await productsRepository.AddAsync(productToCreate, cancellationToken);
            await productsRepository.SaveChangesAsync(cancellationToken);

            return createdProduct;
        }

        public async Task<Product> UpdateAsync(int id, CreateOrUpdateProductRequest request, CancellationToken cancellationToken) {
            var productToUpdate = await productsRepository.GetByIdAsync(id, cancellationToken) 
                ?? throw new InvalidOperationException(
                    $"Product with id = {id} was not found"
                );

            productToUpdate.Name = request.Name;
            productToUpdate.Price = request.Price;
            productToUpdate.Quantity = request.Quantity;

            productsRepository.Update(productToUpdate);
            await productsRepository.SaveChangesAsync(cancellationToken);

            return productToUpdate;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken) {
            var productToDelete = await productsRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new InvalidOperationException(
                    $"Product with id = {id} was not found"
                );

            productsRepository.Delete(productToDelete);
            await productsRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
