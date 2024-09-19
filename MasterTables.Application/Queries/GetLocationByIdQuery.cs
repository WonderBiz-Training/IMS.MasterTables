using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetLocationByIdQuery : IRequest<LocationDto>
    {
        public Guid Id { get; }

        public GetLocationByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
