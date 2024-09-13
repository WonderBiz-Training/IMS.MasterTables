namespace MasterTables.Domain.Events.ProductEvents
{
    public class ProductCreatedEvent
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public double Price { get; }
        public string Code { get; }

        public ProductCreatedEvent(Guid productId, string productName, double price, string code)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Code = code;
        }
    }
}
