using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetVendorByIdQuery : IRequest<VendorDto>
    {
        public Guid Id { get; set; }

        public GetVendorByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
