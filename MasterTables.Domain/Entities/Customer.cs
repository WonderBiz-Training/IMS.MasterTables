using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Entities
{
    public class Customer: BaseEntity
    {
        [Required(ErrorMessage ="Customer Name is required")]
        public string CustomerName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Customer Email is required")]
        public string CustomerEmail { get; set; } = string.Empty;
        [Required(ErrorMessage = "Phone Number is required")]
        public long PhoneNumber { get; set; }

        public bool IsActive { get; set; }
    }
}
