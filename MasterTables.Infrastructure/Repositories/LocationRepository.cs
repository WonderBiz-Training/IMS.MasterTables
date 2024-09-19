using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MasterTables.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MasterTables.Infrastructure.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly MasterTablesDbContext _context;

        public LocationRepository(MasterTablesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken)
        {
            return await _context.Locations.ToListAsync(cancellationToken);
        }

        public async Task<Location> GetLocationByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Locations.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddLocationAsync(Location location, CancellationToken cancellationToken)
        {
            await _context.Locations.AddAsync(location, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateLocationAsync(Location location, CancellationToken cancellationToken)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteLocationAsync(Location location, CancellationToken cancellationToken)
        {
            _context.Locations.Remove(location);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> LocationExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Locations.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
