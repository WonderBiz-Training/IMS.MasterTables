using MediatR;

namespace MasterTables.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
