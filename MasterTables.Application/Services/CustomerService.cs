using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;

namespace MasterTables.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMediator _mediator;

        public CustomerService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetAllCustomerQuery();
                var result = await _mediator.Send(query, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetCustomerByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null)
                {
                    throw new CustomerNotFoundException($"Customer with ID {id} not found.");
                }
                return result;
            }
            catch (CustomerNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new CustomerAlreadyExistsException("Customer already exists.");
                }
                return result;
            }
            catch (CustomerAlreadyExistsException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                // Log exception here
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<CustomerDto> UpdateCustomerAsync(UpdateCustomerCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new CustomerNotFoundException("Customer not found for update.");
                }
                return result;
            }
            catch (CustomerNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<bool> DeleteCustomerAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteCustomerCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);
                if (!result)
                {
                    throw new CustomerNotFoundException("Customer not found for deletion.");
                }
                return result;
            }
            catch (CustomerNotFoundException)
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
