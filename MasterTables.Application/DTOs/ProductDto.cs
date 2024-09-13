namespace MasterTables.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
    }

    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
    }

    public class UpdateProductDto
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
    }

}
