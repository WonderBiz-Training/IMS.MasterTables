using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MasterTables.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MasterTables.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly MasterTablesDbContext _context;

        public VendorRepository(MasterTablesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync(CancellationToken cancellationToken)
        {
            return await _context.Vendors.ToListAsync(cancellationToken);
        }

        public async Task<Vendor> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Vendors.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddVendorAsync(Vendor vendor, CancellationToken cancellationToken)
        {
            await _context.Vendors.AddAsync(vendor, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateVendorAsync(Vendor vendor, CancellationToken cancellationToken)
        {
            _context.Vendors.Update(vendor);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteVendorAsync(Vendor vendor, CancellationToken cancellationToken)
        {
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> VendorExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Vendors.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
