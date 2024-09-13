using MediatR;
using MasterTables.Application.DTOs;

namespace MasterTables.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<CustomerDto>
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
