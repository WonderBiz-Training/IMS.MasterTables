using System.ComponentModel.DataAnnotations;

namespace MasterTables.Application.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "City Name is required")]
        public string CityName { get; set; }
        [Required(ErrorMessage = "State Name is required")]
        public string StateName { get; set; }
        [Required(ErrorMessage = "Country Name is required")]
        public string CountryName { get; set; }
        [Required(ErrorMessage = "Address Line 1 is required")]
        public string AddressLine1 { get; set; }
        [Required(ErrorMessage = "Address Line 2 is required")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "ZipCode is required")]
        public long ZipCode { get; set; }

        public bool IsActive { get; set; }
    }
}
