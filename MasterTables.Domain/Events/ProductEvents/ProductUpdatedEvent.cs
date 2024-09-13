using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Events.ProductEvents
{
    public class ProductUpdatedEvent
    {
        public Guid ProductId { get; }
        public string ProductName { get; }
        public double Price { get; }
        public string Code { get; }

        public ProductUpdatedEvent(Guid productId, string productName, double price, string code)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            Code = code;
        }
    }
}
