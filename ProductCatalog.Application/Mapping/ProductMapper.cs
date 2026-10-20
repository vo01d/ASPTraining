using ProductCatalog.Application.RequestsAndResponses.Products;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Application.Mapping {
    public static class ProductMapper {
        public static Product ToEntity(CreateProductRequest request) {
            return new Product {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };
        }

        public static void Apply(Product productToUpdate, UpdateProductRequest request) {
            productToUpdate.Name = request.Name;
            productToUpdate.Price = request.Price;
            productToUpdate.Quantity = request.Quantity;
        }
    }
}
