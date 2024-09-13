using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetAllTaxesQuery : IRequest<IEnumerable<TaxDto>>
    {
    }

}
