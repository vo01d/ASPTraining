using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.Context.Extensions;

namespace ProductCatalog.Infrastructure.Context {
    public class ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : DbContext(options) {
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Configure();
        }
    }
}
