using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MasterTables.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MasterTables.Infrastructure.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly MasterTablesDbContext _context;

        public TaxRepository(MasterTablesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tax>> GetAllTaxesAsync(CancellationToken cancellationToken)
        {
            return await _context.Taxes.ToListAsync(cancellationToken);
        }

        public async Task<Tax> GetTaxByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Taxes.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddTaxAsync(Tax tax, CancellationToken cancellationToken)
        {
            await _context.Taxes.AddAsync(tax, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateTaxAsync(Tax tax, CancellationToken cancellationToken)
        {
            _context.Taxes.Update(tax);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTaxAsync(Tax tax, CancellationToken cancellationToken)
        {
            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> TaxExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Taxes.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
