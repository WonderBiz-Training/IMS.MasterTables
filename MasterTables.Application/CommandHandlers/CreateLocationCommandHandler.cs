using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;

namespace MasterTables.Application.CommandHandlers
{
    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, LocationDto>
    {
        private readonly ILocationRepository _repository;

        public CreateLocationCommandHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<LocationDto> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location
            {
                CityName = request.CityName,
                StateName = request.StateName,
                CountryName = request.CountryName,
                ZipCode = request.ZipCode,
                IsActive = request.IsActive,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid(),
            };

            await _repository.AddLocationAsync(location, cancellationToken);

            return new LocationDto
            {
                Id = location.Id,
                CityName = request.CityName,
                StateName = request.StateName,
                CountryName = request.CountryName,
                ZipCode= request.ZipCode,
                AddressLine1 = request.AddressLine1,
                AddressLine2 = request.AddressLine2,
                IsActive = request.IsActive
            };
        }
    }
}
