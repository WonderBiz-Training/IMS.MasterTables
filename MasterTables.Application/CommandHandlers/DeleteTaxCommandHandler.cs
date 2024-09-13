using MasterTables.Application.Commands;
using MasterTables.Domain.Exceptions;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.CommandHandlers
{
    public class DeleteTaxCommandHandler : IRequestHandler<DeleteTaxCommand, bool>
    {
        private readonly ITaxRepository _repository;

        public DeleteTaxCommandHandler(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteTaxCommand request, CancellationToken cancellationToken)
        {
            var tax = await _repository.GetTaxByIdAsync(request.Id, cancellationToken);
            if (tax == null)
            {
                throw new TaxNotFoundException("Tax not found");
            }

            await _repository.DeleteTaxAsync(tax, cancellationToken);
            return true;
        }
    }
}
