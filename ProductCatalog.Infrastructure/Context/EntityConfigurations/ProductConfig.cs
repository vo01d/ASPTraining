using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Infrastructure.Context.EntityConfigurations {
    public class ProductConfig : IEntityTypeConfiguration<Product> {
        public void Configure(EntityTypeBuilder<Product> builder) {
            builder.ToTable("products");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.Id).HasColumnName("product_id");
            builder.Property(p => p.Name).HasColumnName("product_name").HasMaxLength(50).IsRequired();
            builder.Property(p => p.Price).HasColumnType("decimal(10,2)").HasColumnName("product_price").IsRequired();
            builder.Property(p => p.Quantity).HasColumnName("product_quantity").IsRequired();

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
