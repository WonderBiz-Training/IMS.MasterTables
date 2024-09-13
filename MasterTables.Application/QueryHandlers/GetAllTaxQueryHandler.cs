using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetAllTaxQueryHandler : IRequestHandler<GetAllTaxesQuery, IEnumerable<TaxDto>>
    {
        private readonly ITaxRepository _repository;

        public GetAllTaxQueryHandler(ITaxRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TaxDto>> Handle(GetAllTaxesQuery request, CancellationToken cancellationToken)
        {
            var taxes = await _repository.GetAllTaxesAsync(cancellationToken);

            return taxes.Select(c => new TaxDto
            {
                Id = c.Id,
                TaxName = c.TaxName,
                Percentage = c.Percentage,
                Code = c.Code,
            });
        }
    }
}
