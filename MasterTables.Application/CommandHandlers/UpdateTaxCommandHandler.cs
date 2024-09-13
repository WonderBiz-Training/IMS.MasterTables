using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, TaxDto>
    {
        private readonly ITaxRepository _repository;

        public UpdateTaxCommandHandler(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<TaxDto> Handle(UpdateTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = await _repository.GetTaxByIdAsync(request.Id, cancellationToken);
            if (tax == null)
            {
                throw new TaxNotFoundException("Tax not found");
            }

            tax.TaxName = request.TaxName;
            tax.Percentage = request.Percentage;
            tax.Code = request.Code;
            tax.IsActive = false;
            tax.UpdatedBy = Guid.NewGuid();
            tax.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateTaxAsync(tax, cancellationToken);

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
