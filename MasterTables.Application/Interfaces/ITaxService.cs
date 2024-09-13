using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Domain.Entities;

namespace MasterTables.Application.Interfaces
{
    public interface ITaxService
    {
        Task<IEnumerable<TaxDto>> GetAllTaxesAsync(CancellationToken cancellationToken = default);
        Task<TaxDto> GetTaxByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<TaxDto> CreateTaxAsync(TaxDto taxDto, CancellationToken cancellationToken = default);
        Task<TaxDto> UpdateTaxAsync(UpdateTaxCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaxAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
