using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;
using System.Security.Cryptography.X509Certificates;
using MasterTables.Domain.Entities;

namespace MasterTables.Application.CommandHandlers
{
    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, LocationDto>
    {
        private readonly ILocationRepository _repository;

        public UpdateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LocationDto> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = await _repository.GetLocationByIdAsync(request.Id, cancellationToken);
            if (location == null)
            {
                throw new LocationNotFoundException("Location not found"); // Custom exception handling can be used
            }

            location.CityName = request.CityName;
            location.StateName = request.StateName;
            location.CountryName = request.CountryName;
            location.ZipCode = request.ZipCode;
            location.IsActive = request.IsActive;
            location.UpdatedBy = Guid.NewGuid(); // Modify as needed
            location.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateLocationAsync(location, cancellationToken);

            return new LocationDto
            {
                Id = location.Id,
                CityName = request.CityName,
                StateName = request.StateName,
                CountryName = request.CountryName,
                ZipCode = request.ZipCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                IsActive = request.IsActive
            };
        }
    }
}
