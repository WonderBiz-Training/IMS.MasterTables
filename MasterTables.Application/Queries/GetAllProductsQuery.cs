using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {
    }

}
