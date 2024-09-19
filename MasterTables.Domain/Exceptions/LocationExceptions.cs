using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Exceptions
{
    public class LocationNotFoundException : Exception
    {
        public LocationNotFoundException(string message) : base(message) { }
    }

    public class LocationAlreadyExistsException : Exception
    {
        public LocationAlreadyExistsException(string? message) : base(message)
        {
        }
    }

    public class LocationValidationException : Exception
    {
        public LocationValidationException(string message) : base(message) { }
    }
}
