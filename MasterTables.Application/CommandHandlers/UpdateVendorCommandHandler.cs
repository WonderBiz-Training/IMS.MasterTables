using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand, VendorDto>
    {
        private readonly IVendorRepository _repository;

        public UpdateVendorCommandHandler(IVendorRepository repository)
        {
            _repository = repository;
        }

        public async Task<VendorDto> Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _repository.GetVendorByIdAsync(request.Id, cancellationToken);
            if (vendor == null)
            {
                throw new VendorNotFoundException("Vendor not found");
            }

            vendor.VendorName = request.VendorName;
            vendor.Address = request.Address;
            vendor.Code = request.Code;
            vendor.ContactPersonName = request.ContactPersonName;
            vendor.ContactPersonPhone = request.ContactPersonPhone;
            vendor.IsActive = false;
            vendor.UpdatedBy = Guid.NewGuid();
            vendor.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateVendorAsync(vendor, cancellationToken);

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
