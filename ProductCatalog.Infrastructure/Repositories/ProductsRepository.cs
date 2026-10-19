using ProductCatalog.Application.Abstractions.Repositories;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.Context;

namespace ProductCatalog.Infrastructure.Repositories {
    public class ProductsRepository(ProductCatalogDbContext context) : Repository<Product>(context), IProductsRepository {
    }
}
