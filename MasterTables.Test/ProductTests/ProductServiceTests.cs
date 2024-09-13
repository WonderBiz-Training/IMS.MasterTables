using Moq;
using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Application.Services;
using MasterTables.Domain.Exceptions;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;
using MediatR;

namespace MasterTables.Test.ProductTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            // Setup Moq for the IMediator
            _mediatorMock = new Mock<IMediator>();

            // Instantiate the service with the mocked mediator
            _productService = new ProductService(_mediatorMock.Object);
        }

        // Test GetAllProductsAsync
        [Fact]
        public async Task GetAllProductsAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product1", Price = 10.0, Code = "P001" },
                new ProductDto { Id = Guid.NewGuid(), ProductName = "Product2", Price = 20.0, Code = "P002" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(products);

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        // Test GetProductByIdAsync - valid product
        [Fact]
        public async Task GetProductByIdAsync_WithValidId_ShouldReturnProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new ProductDto { Id = productId, ProductName = "Product1", Price = 15.0, Code = "P001" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetProductByIdQuery>(q => q.Id == productId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(product);

            // Act
            var result = await _productService.GetProductByIdAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product1", result.ProductName);
        }

        // Test GetProductByIdAsync - product not found
        [Fact]
        public async Task GetProductByIdAsync_WithInvalidId_ShouldThrowProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductDto)null);

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _productService.GetProductByIdAsync(productId));
        }

        // Test CreateProductAsync - successful creation
        [Fact]
        public async Task CreateProductAsync_WithValidData_ShouldReturnProduct()
        {
            // Arrange
            var productDto = new ProductDto { ProductName = "Product1", Price = 25.0, Code = "P003" };
            var createdProduct = new ProductDto { Id = Guid.NewGuid(), ProductName = "Product1", Price = 25.0, Code = "P003" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdProduct);

            // Act
            var result = await _productService.CreateProductAsync(productDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Product1", result.ProductName);
        }

        // Test CreateProductAsync - product already exists
        [Fact]
        public async Task CreateProductAsync_WithDuplicateProduct_ShouldThrowProductAlreadyExistsException()
        {
            // Arrange
            var productDto = new ProductDto { ProductName = "Product1", Price = 25.0, Code = "P003" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((ProductDto)null); // Simulating product already exists

            // Act & Assert
            await Assert.ThrowsAsync<ProductAlreadyExistsException>(() => _productService.CreateProductAsync(productDto));
        }

        // Test DeleteProductAsync - successful deletion
        [Fact]
        public async Task DeleteProductAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _productService.DeleteProductAsync(productId);

            // Assert
            Assert.True(result);
        }

        // Test DeleteProductAsync - product not found
        [Fact]
        public async Task DeleteProductAsync_WithInvalidId_ShouldThrowProductNotFoundException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false); // Simulating product not found

            // Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => _productService.DeleteProductAsync(productId));
        }
    }
}