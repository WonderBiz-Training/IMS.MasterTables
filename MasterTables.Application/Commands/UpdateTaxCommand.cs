using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Commands
{
    public class UpdateTaxCommand : IRequest<TaxDto>
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; }
        public double Percentage { get; set; }
        public string Code { get; set; }

    }

}
