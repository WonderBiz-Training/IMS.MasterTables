using Xunit;
using FluentValidation.TestHelper;
using MasterTables.Application.Validators.CreateCommandValidator;
using MasterTables.Application.DTOs;

namespace MasterTables.Tests
{
    public class CreateProductCommandValidatorTests
    {
        private readonly CreateProductCommandValidator _validator;

        public CreateProductCommandValidatorTests()
        {
            _validator = new CreateProductCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Is_Empty()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = string.Empty,
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductName)
                .WithErrorMessage("Product name is required.");
        }

        [Fact]
        public void Should_Have_Error_When_ProductName_Exceeds_Max_Length()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = new string('A', 21), // 21 characters
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.ProductName)
                .WithErrorMessage("Product name cannot exceed 20 characters.");
        }

        [Fact]
        public void Should_Have_Error_When_Price_Is_Less_Than_Or_Equal_To_Zero()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = "Product1",
                Price = 0, // Invalid price
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Price)
                .WithErrorMessage("Price must be greater than zero.");
        }

        [Fact]
        public void Should_Have_Error_When_Code_Is_Empty()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = "Product1",
                Price = 50,
                Code = string.Empty // Invalid code
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Code)
                .WithErrorMessage("Code is required.");
        }

        [Fact]
        public void Should_Have_Error_When_Code_Exceeds_Max_Length()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = "Product1",
                Price = 50,
                Code = new string('C', 11) // 11 characters, exceeding max length of 10
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldHaveValidationErrorFor(c => c.Code)
                .WithErrorMessage("Code cannot exceed 10 characters.");
        }

        [Fact]
        public void Should_Not_Have_Error_For_Valid_Command()
        {
            // Arrange
            var productdto = new ProductDto
            {
                ProductName = "Product1",
                Price = 50,
                Code = "P001"
            };

            // Act
            var result = _validator.TestValidate(productdto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(c => c.ProductName);
            result.ShouldNotHaveValidationErrorFor(c => c.Price);
            result.ShouldNotHaveValidationErrorFor(c => c.Code);
        }
    }
}
