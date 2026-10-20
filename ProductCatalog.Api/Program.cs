using ProductCatalog.Api.Extensions;
using ProductCatalog.Api.Middleware;

namespace ProductCatalog.API {
    internal class Program {
        private static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddAppDbContext(builder.Configuration);
            builder.Services.AddInfrastructureServices(builder.Configuration);

            builder.Services.AddControllers()
                .AddJsonOptions(
                    options => {
                        options.JsonSerializerOptions.WriteIndented = true;
                    }
                );

            builder.Services.AddCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(opt => opt
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                );

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAppDbContext();

            app.MapControllers();

            app.Run();
        }
    }
}
