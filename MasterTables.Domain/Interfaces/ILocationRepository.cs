using MasterTables.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasterTables.Domain.Interfaces
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocationsAsync(CancellationToken cancellationToken);
        Task<Location> GetLocationByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddLocationAsync(Location location, CancellationToken cancellationToken);
        Task UpdateLocationAsync(Location location, CancellationToken cancellationToken);
        Task DeleteLocationAsync(Location location, CancellationToken cancellationToken);
        Task<bool> LocationExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
