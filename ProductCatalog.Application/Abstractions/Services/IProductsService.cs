using ProductCatalog.Application.RequestsAndResponces.Products;
using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Application.Abstractions.Services {
    public interface IProductsService {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken);

        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Product> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken);

        Task<Product> UpdateAsync(int id, UpdateProductRequest request, CancellationToken cancellationToken);

        Task DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
