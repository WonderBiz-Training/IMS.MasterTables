using FluentValidation;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;

namespace MasterTables.Application.Validators.CreateCommandValidator
{
    public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
    {
        public CreateLocationCommandValidator()
        {
            RuleFor(x => x.CityName)
                .NotEmpty().WithMessage("City name is required.")
                .MaximumLength(100).WithMessage("City name cannot exceed 100 characters.");

            RuleFor(x => x.StateName)
                .NotEmpty().WithMessage("State name is required.")
                .MaximumLength(100).WithMessage("State name cannot exceed 100 characters.");

            RuleFor(x => x.CountryName)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");

            RuleFor(x => x.AddressLine1)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.ZipCode)
                .GreaterThan(0).WithMessage("ZipCode must be a positive number.")
                .Must(zip => zip.ToString().Length == 6).WithMessage("ZipCode must be 6 digits long.");


            RuleFor(x => x.IsActive)
                .NotNull().WithMessage("IsActive status is required.");
        }
    }
}
