using MasterTables.Application.DTOs;
using MasterTables.Application.Queries;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.QueryHandlers
{
    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, LocationDto>
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationByIdQueryHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<LocationDto> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetLocationByIdAsync(request.Id, cancellationToken);
            if (location == null)
            {
                return null; // or throw a NotFoundException
            }

            // Manually map entity to DTO
            return new LocationDto
            {
                Id = location.Id,
                CityName = location.CityName,
                StateName = location.StateName,
                CountryName = location.CountryName,
                IsActive = location.IsActive
            };
        }
    }
}
