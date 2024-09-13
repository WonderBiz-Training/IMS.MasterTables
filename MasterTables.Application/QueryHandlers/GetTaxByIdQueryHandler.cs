using AutoMapper;
using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetTaxByIdQueryHandler : IRequestHandler<GetTaxByIdQuery, TaxDto>
    {
        private readonly ITaxRepository _taxRepository;

        public GetTaxByIdQueryHandler(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public async Task<TaxDto> Handle(GetTaxByIdQuery request, CancellationToken cancellationToken)
        {
            var tax = await _taxRepository.GetTaxByIdAsync(request.Id, cancellationToken);
            if (tax == null)
            {
                return null;
            }

            return new TaxDto
            {
                Id = tax.Id,
                TaxName = tax.TaxName,
                Percentage = tax.Percentage,
                Code = tax.Code,
            };
        }
    }
}
