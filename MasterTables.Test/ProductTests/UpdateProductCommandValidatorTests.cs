using Xunit;
using FluentValidation.TestHelper;
using MasterTables.Application.Commands;
using MasterTables.Application.Validators.UpdateCommandValidator;

namespace MasterTables.Tests
{
    public class UpdateProductCommandValidatorTests
    {
        private readonly UpdateProductCommandValidator _validator;

        public UpdateProductCommandValidatorTests()
        {
            _validator = new UpdateProductCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = string.Empty,
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductName)
                .WithErrorMessage("Product name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Exceeds_Max_Length()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = new string('A', 21), // 21 characters
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductName)
                .WithErrorMessage("Product name cannot exceed 20 characters.");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Less_Than_Or_Equal_To_Zero()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = "Product1",
                Price = 0, // Invalid price
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Price)
                .WithErrorMessage("Price must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_Code_Is_Empty()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = "Product1",
                Price = 50,
                Code = string.Empty // Invalid code
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Code)
                .WithErrorMessage("Code is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Code_Exceeds_Max_Length()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = "Product1",
                Price = 50,
                Code = new string('C', 11) // 11 characters, exceeding max length of 10
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Code)
                .WithErrorMessage("Code cannot exceed 10 characters.");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Command()
        {
            // Arrange
            var command = new UpdateProductCommand
            {
                ProductName = "Product1",
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ProductName);
            result.ShouldNotHaveValidationErrorFor(c => c.Price);
            result.ShouldNotHaveValidationErrorFor(c => c.Code);
        }
    }
}
