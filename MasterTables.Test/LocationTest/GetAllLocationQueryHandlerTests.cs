using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Application.QueryHandlers;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace MasterTables.Test.LocationTest
{
    public class GetAllLocationQueryHandlerTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly GetAllLocationQueryHandler _handler;

        public GetAllLocationQueryHandlerTests()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _handler = new GetAllLocationQueryHandler(_repositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ReturnsListOfLocations()
        {
            // Arrange
            var locations = new List<Location>
            {
                new Location
                {
                    Id = Guid.NewGuid(),
                    CityName = "New York",
                    StateName = "NY",
                    CountryName = "USA",
                    AddressLine1 = "123 Main St",
                    AddressLine2 = "Suite 100",
                    IsActive = true
                },
                new Location
                {
                    Id = Guid.NewGuid(),
                    CityName = "Los Angeles",
                    StateName = "CA",
                    CountryName = "USA",
                    AddressLine1 = "456 Sunset Blvd",
                    AddressLine2 = "Apt 200",
                    IsActive = true
                }
            };

            _repositoryMock.Setup(repo => repo.GetAllLocationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(locations);

            // Act
            var result = await _handler.Handle(new GetAllLocationQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("New York", result.First().CityName);
            Assert.Equal("Los Angeles", result.Last().CityName);
        }

        [Fact]
        public async Task Handle_EmptyList_ReturnsEmptyEnumerable()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetAllLocationsAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Location>());

            // Act
            var result = await _handler.Handle(new GetAllLocationQuery(), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
