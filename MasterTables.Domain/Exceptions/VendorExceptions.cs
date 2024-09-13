namespace MasterTables.Domain.Exceptions
{
    public class VendorNotFoundException : Exception
    {
        public VendorNotFoundException(string message) : base(message) { }
    }

    public class VendorAlreadyExistsException : Exception
    {
        public VendorAlreadyExistsException(string? message) : base(message)
        {
        }
    }

    public class VendorValidationException : Exception
    {
        public VendorValidationException(string message) : base(message) { }
    }
}
