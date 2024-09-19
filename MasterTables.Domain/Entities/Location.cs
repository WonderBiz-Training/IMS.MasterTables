using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Entities
{
    public class Location : BaseEntity
    {
        public string CityName { get; set; } = string.Empty;    
        public string StateName { get; set; }  = string.Empty;
        public string CountryName { get; set; } = string.Empty;

        public string AddressLine1 {  get; set; } = string.Empty;
        public string AddressLine2 { get; set;} = string.Empty;

        public long ZipCode { get; set; }

        public bool IsActive { get; set; }
    }
}
