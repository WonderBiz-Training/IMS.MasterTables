using FluentValidation;
using FluentValidation.TestHelper;
using MasterTables.Application.Commands;
using MasterTables.Application.CommandHandlers;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using MasterTables.Application.Validators.CreateCommandValidator;

namespace MasterTables.Test.LocationTest
{
    public class CreateLocationCommandHandlerTests
    {
        private readonly Mock<ILocationRepository> _repositoryMock;
        private readonly CreateLocationCommandHandler _handler;
        private readonly CreateLocationCommandValidator _validator;

        public CreateLocationCommandHandlerTests()
        {
            _repositoryMock = new Mock<ILocationRepository>();
            _handler = new CreateLocationCommandHandler(_repositoryMock.Object);
            _validator = new CreateLocationCommandValidator();
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldReturnLocationDto()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
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
                Id = Guid.NewGuid(),
                CityName = command.CityName,
                StateName = command.StateName,
                CountryName = command.CountryName,
                ZipCode = command.ZipCode,
                IsActive = command.IsActive
            };

            _repositoryMock.Setup(repo => repo.AddLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(command.CityName, result.CityName);
            _repositoryMock.Verify(repo => repo.AddLocationAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void Validate_EmptyCityName_ShouldHaveError()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
                CityName = string.Empty,
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
            var command = new CreateLocationCommand
            {
                CityName = "New York",
                StateName = string.Empty,
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
        public void Validate_EmptyCountryName_ShouldHaveError()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
                CityName = "New York",
                StateName = "NY",
                CountryName = string.Empty,
                AddressLine1 = "123 Main St",
                ZipCode = 123456,
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.CountryName).WithErrorMessage("Country name is required.");
        }

        [Fact]
        public void Validate_EmptyAddressLine1_ShouldHaveError()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = string.Empty,
                ZipCode = 123456,
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.AddressLine1).WithErrorMessage("Address is required.");
        }

        [Fact]
        public void Validate_EmptyZipCode_ShouldHaveError()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 0,  // Invalid ZipCode (Empty or 0)
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ZipCode).WithErrorMessage("ZipCode must be a positive number.");
        }

        [Fact]
        public void Validate_InvalidZipCode_ShouldHaveError()
        {
            // Arrange
            var command = new CreateLocationCommand
            {
                CityName = "New York",
                StateName = "NY",
                CountryName = "USA",
                AddressLine1 = "123 Main St",
                ZipCode = 12345,  // Less than 6 digits (invalid)
                IsActive = true
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ZipCode).WithErrorMessage("ZipCode must be 6 digits long.");
        }
    }
}
