using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
