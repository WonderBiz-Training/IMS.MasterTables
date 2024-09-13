using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;

namespace MasterTables.Application.CommandHandlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                CustomerName = request.CustomerName,
                CustomerEmail = request.CustomerEmail,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.IsActive,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid(),
            };

            await _repository.AddCustomerAsync(customer, cancellationToken);

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
