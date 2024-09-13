using MasterTables.Application.DTOs;
using MasterTables.Application.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasterTables.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken cancellationToken = default);
        Task<CustomerDto> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default);
        Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command, CancellationToken cancellationToken = default);
        Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
