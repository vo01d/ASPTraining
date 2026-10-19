using ProductCatalog.Core.Entities;
using System.Text;

namespace ProductCatalog.Application.RequestsAndResponces.Products {
    public class CreateOrUpdateProductRequest {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public static Product ToDbModel(CreateOrUpdateProductRequest request) {
            return new Product {
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };
        }
    }
}
