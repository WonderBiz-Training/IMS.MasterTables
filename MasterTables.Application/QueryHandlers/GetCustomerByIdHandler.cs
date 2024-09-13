using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(request.Id, cancellationToken);
            if (customer == null)
            {
                return null; // or throw a NotFoundException
            }

            // Manually map entity to DTO
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
