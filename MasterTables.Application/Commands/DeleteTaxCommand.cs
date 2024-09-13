using MediatR;

namespace MasterTables.Application.Commands
{
    public class DeleteTaxCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

}
