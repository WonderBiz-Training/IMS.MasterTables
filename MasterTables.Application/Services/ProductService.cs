using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;

namespace MasterTables.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;

        public ProductService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetAllProductsQuery();
                var result = await _mediator.Send(query, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<ProductDto> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetProductByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null)
                {
                    throw new ProductNotFoundException($"Product with ID {id} not found.");
                }
                return result;
            }
            catch (ProductNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new CreateProductCommand
                {
                    ProductName = productDto.ProductName,
                    Price = productDto.Price,
                    Code = productDto.Code,
                };
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new ProductAlreadyExistsException("Product already exists.");
                }
                return result;
            }
            catch (ProductAlreadyExistsException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new ProductNotFoundException("Product not found for update.");
                }
                return result;
            }
            catch (ProductNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteProductCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);
                if (!result)
                {
                    throw new ProductNotFoundException("Product not found for deletion.");
                }
                return result;
            }
            catch (ProductNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }
    }
}
