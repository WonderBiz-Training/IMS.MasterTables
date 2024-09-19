using MasterTables.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Application.Commands
{
    public class CreateLocationCommand : IRequest<LocationDto>
    {
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public long ZipCode { get; set; }

        public bool IsActive { get; set; }
    }
}
