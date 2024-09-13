using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Commands
{
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }

    }

}
