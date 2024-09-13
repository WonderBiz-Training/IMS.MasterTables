using MasterTables.Domain.Entities;

namespace MasterTables.Domain.Interfaces
{
    public interface IVendorRepository
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync(CancellationToken cancellationToken);
        Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddVendorAsync(Vendor vendor, CancellationToken cancellationToken);
        Task UpdateVendorAsync(Vendor vendor, CancellationToken cancellationToken);
        Task DeleteVendorAsync(Vendor vendor, CancellationToken cancellationToken);
        Task<bool> VendorExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
