using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;

namespace MasterTables.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductDto> GetProductByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ProductDto> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken = default);
        Task<ProductDto> UpdateProductAsync(UpdateProductCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteProductAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
