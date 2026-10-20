using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Api.Middleware {
    public sealed class ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    ) {
        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            }
            catch (Exception ex) {
                logger.LogError(ex, "An unexpected error occurred while processing the request.");

                if (context.Response.HasStarted) {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "An unexpected error occurred.",
                    Detail = "The server could not complete the request."
                };

                problem.Extensions["traceId"] = context.TraceIdentifier;

                await context.Response.WriteAsJsonAsync(
                    problem,
                    cancellationToken: context.RequestAborted
                );
            }
        }
    }
}