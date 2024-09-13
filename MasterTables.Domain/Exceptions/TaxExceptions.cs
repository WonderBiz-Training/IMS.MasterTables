namespace MasterTables.Domain.Exceptions
{
    public class TaxNotFoundException : Exception
    {
        public TaxNotFoundException(string message) : base(message) { }
    }

    public class TaxAlreadyExistsException : Exception
    {
        public TaxAlreadyExistsException(string? message) : base(message)
        {
        }
    }

    public class TaxValidationException : Exception
    {
        public TaxValidationException(string message) : base(message) { }
    }
}
