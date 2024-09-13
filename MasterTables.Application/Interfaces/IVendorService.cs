using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;

namespace MasterTables.Application.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<VendorDto>> GetAllVendorsAsync(CancellationToken cancellationToken = default);
        Task<VendorDto> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<VendorDto> CreateVendorAsync(VendorDto vendorDto, CancellationToken cancellationToken = default);
        Task<VendorDto> UpdateVendorAsync(UpdateVendorCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteVendorAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
