using MasterTables.Application.Commands;
using MasterTables.Application.CommandHandlers;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Exceptions;
using MasterTables.Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MasterTables.Test.LocationTest
{
    public class DeleteLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly DeleteLocationCommandHandler _handler;

        public DeleteLocationCommandHandlerTests()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _handler = new DeleteLocationCommandHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldDeleteLocationAndReturnTrue()
        {
            // Arrange
            var command = new DeleteLocationCommand
            {
                Id = Guid.NewGuid()
            };

            var location = new Location
            {
                Id = command.Id,
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                ZipCode = 123456,
                IsActive = true
            };

            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(location);

            _repositoryMock.Setup(repo => repo.DeleteLocationAsync(location, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(repo => repo.DeleteLocationAsync(location, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_LocationNotFound_ShouldThrowLocationNotFoundException()
        {
            // Arrange
            var command = new DeleteLocationCommand
            {
                Id = Guid.NewGuid()
            };

            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Location)null);

            // Act & Assert
            await Assert.ThrowsAsync<LocationNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
