using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;

namespace MasterTables.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMediator _mediator;

        public LocationService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<LocationDto>> GetAllLocationsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetAllLocationQuery();
                var result = await _mediator.Send(query, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<LocationDto> GetLocationByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetLocationByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null)
                {
                    throw new LocationNotFoundException($"Location with ID {id} not found.");
                }
                return result;
            }
            catch (LocationNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<LocationDto> CreateLocationAsync(CreateLocationCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new LocationAlreadyExistsException("Location already exists.");
                }
                return result;
            }
            catch (LocationAlreadyExistsException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<LocationDto> UpdateLocationAsync(UpdateLocationCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new LocationNotFoundException("Location not found for update.");
                }
                return result;
            }
            catch (LocationNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<bool> DeleteLocationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteLocationCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);
                if (!result)
                {
                    throw new LocationNotFoundException("Location not found for deletion.");
                }
                return result;
            }
            catch (LocationNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }
    }
}
