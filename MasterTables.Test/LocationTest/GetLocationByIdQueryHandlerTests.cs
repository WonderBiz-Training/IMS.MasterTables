using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Application.QueryHandlers;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MasterTables.Test.LocationTest
{
    public class GetLocationByIdQueryHandlerTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly GetLocationByIdQueryHandler _handler;

        public GetLocationByIdQueryHandlerTests()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _handler = new GetLocationByIdQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ValidId_ReturnsLocationDto()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            var location = new Location
            {
                Id = locationId,
                CityName = "Chicago",
                StateName = "IL",
                CountryName = "USA",
                IsActive = true
            };

            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(locationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(location);

            var query = new GetLocationByIdQuery(locationId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(locationId, result.Id);
            Assert.Equal("Chicago", result.CityName);
            Assert.Equal("IL", result.StateName);
            Assert.Equal("USA", result.CountryName);
            Assert.True(result.IsActive);
        }

        [Fact]
        public async Task Handle_InvalidId_ReturnsNull()
        {
            // Arrange
            var locationId = Guid.NewGuid();
            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(locationId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Location)null);

            var query = new GetLocationByIdQuery(locationId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
