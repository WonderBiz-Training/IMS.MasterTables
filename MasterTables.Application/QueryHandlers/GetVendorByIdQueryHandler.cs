using AutoMapper;
using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetVendorByIdQueryHandler : IRequestHandler<GetVendorByIdQuery, VendorDto>
    {
        private readonly IVendorRepository _vendorRepository;

        public GetVendorByIdQueryHandler(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<VendorDto> Handle(GetVendorByIdQuery request, CancellationToken cancellationToken)
        {
            var vendor = await _vendorRepository.GetVendorByIdAsync(request.Id, cancellationToken);
            if (vendor == null)
            {
                return null;
            }

            return new VendorDto
            {
                Id = vendor.Id,
                VendorName = vendor.VendorName,
                Address = vendor.Address,
                Code = vendor.Code,
                ContactPersonName = vendor.ContactPersonName,
                ContactPersonPhone = vendor.ContactPersonPhone,
            };
        }
    }
}
