using System.Net;
using System.Threading.Tasks;
using MasterTables.Api.Middlewares;
using MasterTables.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace MasterTables.Tests.Middlewares
{
    public class ExceptionHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_Should_Return_InternalServerError_On_Exception()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            var middleware = new ExceptionHandlingMiddleware(async (innerHttpContext) =>
            {
                throw new System.Exception("Test exception");
            }, mockLogger.Object);

            var context = new DefaultHttpContext();
            var responseStream = new System.IO.MemoryStream();
            context.Response.Body = responseStream;

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);

            context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
            var body = new System.IO.StreamReader(context.Response.Body).ReadToEnd();
            Assert.Contains("An unexpected error occurred", body);
        }

        [Fact]
        public async Task InvokeAsync_Should_Return_NotFound_On_ProductNotFoundException()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            var middleware = new ExceptionHandlingMiddleware(async (innerHttpContext) =>
            {
                throw new ProductNotFoundException("Product not found");
            }, mockLogger.Object);

            var context = new DefaultHttpContext();
            var responseStream = new System.IO.MemoryStream();
            context.Response.Body = responseStream;

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);

            context.Response.Body.Seek(0, System.IO.SeekOrigin.Begin);
            var body = new System.IO.StreamReader(context.Response.Body).ReadToEnd();
            Assert.Contains("Product not found", body);
        }
    }
}
