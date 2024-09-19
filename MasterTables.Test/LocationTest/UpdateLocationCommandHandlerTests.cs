using FluentValidation.TestHelper;
using MasterTables.Application.Commands;
using MasterTables.Application.CommandHandlers;
using MasterTables.Application.DTOs;
using MasterTables.Application.Validators.UpdateCommandValidator;
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
    public class UpdateLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly UpdateLocationCommandHandler _handler;
        private readonly UpdateLocationCommandValidator _validator;

        public UpdateLocationCommandHandlerTests()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _handler = new UpdateLocationCommandHandler(_repositoryMock.Object);
            _validator = new UpdateLocationCommandValidator();
        }

        #region CommandHandler Tests

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateLocationAndReturnDto()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                AddressLine2 = "Apt 4B",
                ZipCode = 123456,
                IsActive = true
            };

            var location = new Location
            {
                Id = command.Id,
                CityName = "Old City",
                StateName = "Old State",
                CountryName = "Old Country",
                ZipCode = 654321,
                IsActive = false
            };

            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(location);

            _repositoryMock.Setup(repo => repo.UpdateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CityName, result.CityName);
            Assert.Equal(command.StateName, result.StateName);
            Assert.Equal(command.CountryName, result.CountryName);
            _repositoryMock.Verify(repo => repo.UpdateLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_LocationNotFound_ShouldThrowLocationNotFoundException()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                ZipCode = 123456,
                IsActive = true
            };

            _repositoryMock.Setup(repo => repo.GetLocationByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Location)null);

            // Act & Assert
            await Assert.ThrowsAsync<LocationNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        #endregion

        #region Validator Tests

        [Fact]
        public void Validate_EmptyId_ShouldHaveError()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.Empty,  // Invalid Id
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 123456,
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Id).WithErrorMessage("Id is required.");
        }

        [Fact]
        public void Validate_EmptyCityName_ShouldHaveError()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = string.Empty,  // Invalid CityName
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 123456,
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CityName).WithErrorMessage("City name is required.");
        }

        [Fact]
        public void Validate_EmptyStateName_ShouldHaveError()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = "New York",
                StateName = string.Empty,  // Invalid StateName
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 123456,
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.StateName).WithErrorMessage("State name is required.");
        }

        [Fact]
        public void Validate_InvalidZipCode_ShouldHaveError()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 12345,  // Invalid ZipCode (less than 6 digits)
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ZipCode).WithErrorMessage("ZipCode must be 6 digits long.");
        }

        [Fact]
        public void Validate_ValidCommand_ShouldNotHaveValidationError()
        {
            // Arrange
            var command = new UpdateLocationCommand
            {
                Id = Guid.NewGuid(),
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 123456,  // Valid ZipCode
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ZipCode);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}
