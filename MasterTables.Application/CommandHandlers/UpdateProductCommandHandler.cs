using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Interfaces;
using MasterTables.Domain.Exceptions;

namespace MasterTables.Application.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetProductByIdAsync(request.Id, cancellationToken);
            if (product == null)
            {
                throw new ProductNotFoundException("Product not found"); 
            }

            product.ProductName = request.ProductName;
            product.Price = request.Price;
            product.Code = request.Code;
            product.IsActive = false;
            product.UpdatedBy = Guid.NewGuid();
            product.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateProductAsync(product, cancellationToken);

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
