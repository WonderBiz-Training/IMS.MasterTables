using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }

    public class CustomerAlreadyExistsException : Exception
    {
        public CustomerAlreadyExistsException(string? message) : base(message)
        {
        }
    }

    public class CustomerValidationException : Exception
    {
        public CustomerValidationException(string message) : base(message) { }
    }
}
