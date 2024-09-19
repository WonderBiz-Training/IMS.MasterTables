using MasterTables.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Application.Commands
{
    public class UpdateLocationCommand : IRequest<LocationDto>
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long ZipCode { get; set; }

        public bool IsActive { get; set; }
    }
}
