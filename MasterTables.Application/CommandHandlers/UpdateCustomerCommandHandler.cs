using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetCustomerByIdAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                throw new CustomerNotFoundException("Customer not found"); // Custom exception handling can be used
            }

            customer.CustomerName = request.CustomerName;
            customer.CustomerEmail = request.CustomerEmail;
            customer.PhoneNumber = request.PhoneNumber;
            customer.IsActive = request.IsActive;
            customer.UpdatedBy = Guid.NewGuid(); // Modify as needed
            customer.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateCustomerAsync(customer, cancellationToken);

            return new CustomerDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                CustomerEmail = customer.CustomerEmail,
                PhoneNumber = customer.PhoneNumber,
                IsActive = customer.IsActive
            };
        }
    }
}
