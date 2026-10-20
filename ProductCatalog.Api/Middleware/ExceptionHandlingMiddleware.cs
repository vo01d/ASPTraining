using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Core.Exceptions;
using ProductCatalog.Core.Utils;

namespace ProductCatalog.Api.Middleware {
    public sealed class ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger
    ) {
        public async Task InvokeAsync(HttpContext context) {
            try {
                await next(context);
            }
            catch (RequestValidationException ex) {
                logger.LogWarning(ex, Messages.RequestValidationFailedMessage);
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails {
                        Status = StatusCodes.Status400BadRequest,
                        Title = Messages.RequestValidationFailedMessage,
                        Detail = ex.Message
                    },
                    cancellationToken: context.RequestAborted);
            }
            catch (EntityNotFoundException ex) {
                logger.LogWarning(ex, Messages.EntityNotFoundMessage);
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsJsonAsync(
                    new ProblemDetails {
                        Status = StatusCodes.Status404NotFound,
                        Title = Messages.EntityNotFoundMessage,
                        Detail = ex.Message
                    },
                    cancellationToken: context.RequestAborted);
            }
            catch (Exception ex) {
                logger.LogError(ex, Messages.UnexpectedErrorMessage);

                if (context.Response.HasStarted) {
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = "application/problem+json";

                var problem = new ProblemDetails {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = Messages.UnexpectedErrorMessage,
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