using FluentValidation;
using MasterTables.Application.Commands;

namespace MasterTables.Application.Validators.UpdateCommandValidator
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {

            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(20).WithMessage("Product name cannot exceed 20 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .MaximumLength(10).WithMessage("Code cannot exceed 10 characters.");
        }
    }

}
