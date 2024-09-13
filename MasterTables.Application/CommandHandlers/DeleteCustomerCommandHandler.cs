using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetCustomerByIdAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer not found");
            }

            await _repository.DeleteCustomerAsync(customer, cancellationToken);
            return true;
        }
    }
}
