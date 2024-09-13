using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetAllVendorQueryHandler : IRequestHandler<GetAllVendorsQuery, IEnumerable<VendorDto>>
    {
        private readonly IVendorRepository _repository;

        public GetAllVendorQueryHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<VendorDto>> Handle(GetAllVendorsQuery request, CancellationToken cancellationToken)
        {
            var vendors = await _repository.GetAllVendorsAsync(cancellationToken);

            return vendors.Select(c => new VendorDto
            {
                Id = c.Id,
                VendorName = c.VendorName,
                Address = c.Address,
                Code = c.Code,
                ContactPersonName = c.ContactPersonName,
                ContactPersonPhone = c.ContactPersonPhone,
            });
        }
    }
}
