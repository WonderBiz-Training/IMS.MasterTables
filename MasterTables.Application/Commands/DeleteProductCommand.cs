using MediatR;

namespace MasterTables.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

}
