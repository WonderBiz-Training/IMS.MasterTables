using MediatR;
using MasterTables.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MasterTables.Domain.Interfaces;
using MasterTables.Application.Queries;


namespace MasterTables.Application.QueryHandlers
{
    public class GetAllCustomerQueryHandler : IRequestHandler<GetAllCustomerQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerRepository _repository;

        public GetAllCustomerQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllCustomersAsync(cancellationToken);

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                CustomerEmail = c.CustomerEmail,
                PhoneNumber = c.PhoneNumber,
                IsActive = c.IsActive
            });
        }
    }
}
