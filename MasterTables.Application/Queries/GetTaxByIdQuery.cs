using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Queries
{
    public class GetTaxByIdQuery : IRequest<TaxDto>
    {
        public Guid Id { get; set; }

        public GetTaxByIdQuery(Guid id)
        {
            Id = id;
        }
    }

}
