using MasterTables.Domain.Entities;
using MasterTables.Domain.Interfaces;
using MasterTables.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MasterTables.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MasterTablesDbContext _context;

        public ProductRepository(MasterTablesDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> ProductExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Products.AnyAsync(c => c.Id == id, cancellationToken);
        }
    }
}
