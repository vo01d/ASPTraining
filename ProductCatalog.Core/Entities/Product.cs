using ProductCatalog.Core.Entities.Abstractions;

namespace ProductCatalog.Core.Entities {
    public class Product : Entity {
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}
