using MasterTables.Domain.Entities;

namespace MasterTables.Domain.Interfaces
{
    public interface ITaxRepository
    {
        Task<IEnumerable<Tax>> GetAllTaxesAsync(CancellationToken cancellationToken);
        Task<Tax> GetTaxByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddTaxAsync(Tax tax, CancellationToken cancellationToken);
        Task UpdateTaxAsync(Tax tax, CancellationToken cancellationToken);
        Task DeleteTaxAsync(Tax tax, CancellationToken cancellationToken);
        Task<bool> TaxExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
