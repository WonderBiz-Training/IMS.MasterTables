using MediatR;
using MasterTables.Application.DTOs;

namespace MasterTables.Application.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDto>
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
