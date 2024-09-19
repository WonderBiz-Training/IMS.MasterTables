using MasterTables.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace MasterTables.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(httpContext);
                //throw new Exception("Test exception - Middleware error handling");
            }
            catch (Exception ex)
            {
                // Log the exception and handle it
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";

            // Set default status code to 500
            var statusCode = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            // Handle custom domain exceptions
            if (exception is ProductNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            else if (exception is ProductAlreadyExistsException)
            {
                statusCode = HttpStatusCode.Conflict;
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }
            else
            {
                // For other unexpected exceptions
                result = JsonSerializer.Serialize(new { error = "An unexpected error occurred. Please try again later." });
            }

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
