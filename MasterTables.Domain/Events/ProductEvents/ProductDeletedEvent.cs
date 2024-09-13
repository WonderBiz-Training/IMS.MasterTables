using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterTables.Domain.Events.ProductEvents
{
    public class ProductDeletedEvent
    {
        public Guid ProductId { get; }

        public ProductDeletedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }

}
