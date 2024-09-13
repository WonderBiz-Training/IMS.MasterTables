using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.CommandHandlers
{
    public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, TaxDto>
    {
        private readonly ITaxRepository _repository;

        public CreateTaxCommandHandler(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxDto> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = new Tax
            {
                TaxName = request.TaxName,
                Percentage = request.Percentage,
                Code = request.Code,
                IsActive = false,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid()
            };

            await _repository.AddTaxAsync(tax, cancellationToken);

            return new TaxDto
            {
                Id = tax.Id,
                TaxName = tax.TaxName,
                Percentage = tax.Percentage,
                Code = tax.Code,
            };
        }
    }
}
