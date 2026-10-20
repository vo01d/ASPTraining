using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Exceptions;
using System.Text;

namespace ProductCatalog.Application.RequestsAndResponses.Products {
    public class CreateProductRequest : IValidatableRequest {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public void Validate() {
            var errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(Name)) {
                errors.AppendLine("Name cannot be empty.");
            }

            if (Price <= 0) {
                errors.AppendLine("Price cannot be less than zero.");
            }

            if (Quantity < 0) {
                errors.AppendLine("Quantity cannot be less than zero.");
            }

            if (errors.Length > 0) {
                throw new RequestValidationException(errors.ToString().TrimEnd());
            }
        }
    }
}
