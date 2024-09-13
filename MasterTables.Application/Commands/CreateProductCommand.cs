using MasterTables.Application.DTOs;
using MediatR;

namespace MasterTables.Application.Commands
{
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }

    }

}
