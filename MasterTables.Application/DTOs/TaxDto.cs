namespace MasterTables.Application.DTOs
{
    public class TaxDto
    {
        public Guid Id { get; set; }
        public string TaxName { get; set; }
        public double Percentage { get; set; }
        public string Code { get; set; }
    }

}
