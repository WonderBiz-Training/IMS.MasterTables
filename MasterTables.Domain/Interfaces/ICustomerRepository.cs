using MasterTables.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MasterTables.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync(CancellationToken cancellationToken);
        Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task AddCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task UpdateCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task DeleteCustomerAsync(Customer customer, CancellationToken cancellationToken);
        Task<bool> CustomerExistsAsync(Guid id, CancellationToken cancellationToken);
    }
}
