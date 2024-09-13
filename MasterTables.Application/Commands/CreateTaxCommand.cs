using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Commands
{
    public class CreateTaxCommand : IRequest<TaxDto>
    {
        public string TaxName { get; set; }
        public double Percentage { get; set; }
        public string Code { get; set; }

    }

}
