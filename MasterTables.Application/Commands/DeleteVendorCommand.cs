using MediatR;

namespace MasterTables.Application.Commands
{
    public class DeleteVendorCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

}
