namespace MasterTables.Domain.Entities
{
    public class Tax : BaseEntity
    {
        public string TaxName { get; set; } = string.Empty;
        public double Percentage { get; set; }
        public string Code { get; set; } = string.Empty;
    }

}
