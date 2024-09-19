using MediatR;
using MasterTables.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MasterTables.Domain.Interfaces;
using MasterTables.Application.Queries;
using MasterTables.Domain.Entities;


namespace MasterTables.Application.QueryHandlers
{
    public class GetAllLocationQueryHandler : IRequestHandler<GetAllLocationQuery, IEnumerable<LocationDto>>
    {
        private readonly ILocationRepository _repository;

        public GetAllLocationQueryHandler(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<LocationDto>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
        {
            var customers = await _repository.GetAllLocationsAsync(cancellationToken);

            return customers.Select(c => new LocationDto
            {
                Id = c.Id,
                CityName = c.CityName,
                StateName = c.StateName,
                CountryName = c.CountryName,
                AddressLine1 = c.AddressLine1,
                AddressLine2 = c.AddressLine2,
                IsActive = c.IsActive
            });
        }
    }
}
