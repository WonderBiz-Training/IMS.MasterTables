using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MediatR;

namespace MasterTables.Application.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Code = request.Code,
                IsActive = false,
                CreatedBy = Guid.NewGuid(),
                UpdatedBy = Guid.NewGuid()
            };

            await _repository.AddProductAsync(product, cancellationToken);

            return new ProductDto
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                Code = product.Code,
            };
        }
    }
}
