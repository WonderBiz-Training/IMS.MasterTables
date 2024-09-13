using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.CommandHandlers
{
    public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand, VendorDto>
    {
        private readonly IVendorRepository _repository;

        public CreateVendorCommandHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<VendorDto> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = new Vendor
            {

                VendorName = request.VendorName,
                Address = request.Address,
                Code = request.Code,
                ContactPersonName = request.ContactPersonName,
                ContactPersonPhone = request.ContactPersonPhone,
                IsActive = false,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid()
            };

            await _repository.AddVendorAsync(vendor, cancellationToken);

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
