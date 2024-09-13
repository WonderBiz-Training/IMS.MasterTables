using MasterTables.Domain.Entities;

namespace MasterTables.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken);
        Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);
        Task<bool> ProductExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
