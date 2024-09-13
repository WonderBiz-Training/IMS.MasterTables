using Moq;
using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Application.Services;
using MasterTables.Domain.Exceptions;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;
using MediatR;

namespace MasterTables.Test.VendorTests
{
    public class VendorServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IVendorService _vendorService;

        public VendorServiceTests()
        {
            // Setup Moq for the IMediator
            _mediatorMock = new Mock<IMediator>();

            // Instantiate the service with the mocked mediator
            _vendorService = new VendorService(_mediatorMock.Object);
        }

        // Test GetAllVendorsAsync
        [Fact]
        public async Task GetAllVendorsAsync_ShouldReturnAllVendors()
        {
            // Arrange
            var vendors = new List<VendorDto>
            {
                new VendorDto { Id = Guid.NewGuid(), VendorName = "Vendor1", Address = "Thane", Code = "V001", ContactPersonName = "John", ContactPersonPhone = "9876543210" },
                new VendorDto { Id = Guid.NewGuid(), VendorName = "Vendor2", Address = "Airoli", Code = "V002", ContactPersonName = "Doe", ContactPersonPhone = "9876549210" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllVendorsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(vendors);

            // Act
            var result = await _vendorService.GetAllVendorsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        // Test GetVendorByIdAsync - valid vendor
        [Fact]
        public async Task GetVendorByIdAsync_WithValidId_ShouldReturnVendor()
        {
            // Arrange
            var vendorId = Guid.NewGuid();
            var vendor = new VendorDto { Id = vendorId, VendorName = "Vendor1", Address = "Thane", Code = "V001", ContactPersonName = "John", ContactPersonPhone = "9876543210" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetVendorByIdQuery>(q => q.Id == vendorId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(vendor);

            // Act
            var result = await _vendorService.GetVendorByIdAsync(vendorId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Vendor1", result.VendorName);
        }

        // Test GetVendorByIdAsync - vendor not found
        [Fact]
        public async Task GetVendorByIdAsync_WithInvalidId_ShouldThrowVendorNotFoundException()
        {
            // Arrange
            var vendorId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetVendorByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((VendorDto)null);

            // Act & Assert
            await Assert.ThrowsAsync<VendorNotFoundException>(() => _vendorService.GetVendorByIdAsync(vendorId));
        }

        // Test CreateVendorAsync - successful creation
        [Fact]
        public async Task CreateVendorAsync_WithValidData_ShouldReturnVendor()
        {
            // Arrange
            var vendorDto = new VendorDto { VendorName = "Vendor1", Address = "Thane", Code = "V001", ContactPersonName = "John", ContactPersonPhone = "9876543210" };
            var createdVendor = new VendorDto { Id = Guid.NewGuid(), VendorName = "Vendor1", Address = "Thane", Code = "V001", ContactPersonName = "John", ContactPersonPhone = "9876543210" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateVendorCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(createdVendor);

            // Act
            var result = await _vendorService.CreateVendorAsync(vendorDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Vendor1", result.VendorName);
        }

        // Test CreateVendorAsync - vendor already exists
        [Fact]
        public async Task CreateVendorAsync_WithDuplicateVendor_ShouldThrowVendorAlreadyExistsException()
        {
            // Arrange
            var vendorDto = new VendorDto { VendorName = "Vendor1", Address = "Thane", Code = "V001", ContactPersonName = "John", ContactPersonPhone = "9876543210" };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateVendorCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((VendorDto)null); // Simulating vendor already exists

            // Act & Assert
            await Assert.ThrowsAsync<VendorAlreadyExistsException>(() => _vendorService.CreateVendorAsync(vendorDto));
        }

        // Test DeleteVendorAsync - successful deletion
        [Fact]
        public async Task DeleteVendorAsync_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var vendorId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteVendorCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            // Act
            var result = await _vendorService.DeleteVendorAsync(vendorId);

            // Assert
            Assert.True(result);
        }

        // Test DeleteVendorAsync - vendor not found
        [Fact]
        public async Task DeleteVendorAsync_WithInvalidId_ShouldThrowVendorNotFoundException()
        {
            // Arrange
            var vendorId = Guid.NewGuid();

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<DeleteVendorCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false); // Simulating vendor not found

            // Act & Assert
            await Assert.ThrowsAsync<VendorNotFoundException>(() => _vendorService.DeleteVendorAsync(vendorId));
        }
    }
}