using System.ComponentModel.DataAnnotations;

namespace MasterTables.Application.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Customer Email is required")]
        public string CustomerEmail { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public long PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
