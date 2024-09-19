using MasterTables.Application.DTOs;
using MasterTables.Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasterTables.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDto>> GetAllLocationsAsync(CancellationToken cancellationToken = default);
        Task<LocationDto> GetLocationByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<LocationDto> CreateLocationAsync(CreateLocationCommand command, CancellationToken cancellationToken = default);
        Task<LocationDto> UpdateLocationAsync(UpdateLocationCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteLocationAsync(Guid id, CancellationToken cancellationToken = default);
    }
}