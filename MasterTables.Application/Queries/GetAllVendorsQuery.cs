using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetAllVendorsQuery : IRequest<IEnumerable<VendorDto>>
    {
    }

}
