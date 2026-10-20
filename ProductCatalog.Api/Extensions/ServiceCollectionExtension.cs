using Microsoft.EntityFrameworkCore;
using ProductCatalog.Application.Abstractions.Repositories;
using ProductCatalog.Application.Abstractions.Services;
using ProductCatalog.Application.Implementations.Services;
using ProductCatalog.Infrastructure.Context;
using ProductCatalog.Infrastructure.Repositories;

namespace ProductCatalog.Api.Extensions {
    public static class ServiceCollectionExtension {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<IProductsService, ProductsService>();
        }
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration) {
            var connectionString = configuration.GetConnectionString("DbConnection");
            services.AddDbContext<ProductCatalogDbContext>(options => options.UseSqlServer(connectionString));
        }

        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IProductsRepository, ProductsRepository>();
        }
    }
}
