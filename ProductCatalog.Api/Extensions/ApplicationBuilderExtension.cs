using Microsoft.EntityFrameworkCore;
using ProductCatalog.Infrastructure.Context;

namespace ProductCatalog.Api.Extensions {
    public static class ApplicationBuilderExtension {
        public static void UseAppDbContext(this IApplicationBuilder app) {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
            using var context = scope?.ServiceProvider.GetRequiredService<ProductCatalogDbContext>();

            context?.Database.Migrate();
        }
    }
}
