using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ProductCatalog.Infrastructure.Context.Extensions {
    public static class ModelBuilderExtensions {
        public static void Configure(this ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
