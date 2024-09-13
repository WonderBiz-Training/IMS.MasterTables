using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using MediatR;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;

namespace MasterTables.Application.Services
{
    public class VendorService : IVendorService
    {
        private readonly IMediator _mediator;

        public VendorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<VendorDto>> GetAllVendorsAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetAllVendorsQuery();
                var result = await _mediator.Send(query, cancellationToken);
                return result;
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<VendorDto> GetVendorByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var query = new GetVendorByIdQuery(id);
                var result = await _mediator.Send(query, cancellationToken);
                if (result == null)
                {
                    throw new VendorNotFoundException($"Vendor with ID {id} not found.");
                }
                return result;
            }
            catch (VendorNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<VendorDto> CreateVendorAsync(VendorDto vendorDto, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new CreateVendorCommand
                {
                    VendorName = vendorDto.VendorName,
                    Address = vendorDto.Address,
                    Code = vendorDto.Code,
                    ContactPersonName = vendorDto.ContactPersonName,
                    ContactPersonPhone = vendorDto.ContactPersonPhone,
                };
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new VendorAlreadyExistsException("Vendor already exists.");
                }
                return result;
            }
            catch (VendorAlreadyExistsException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<VendorDto> UpdateVendorAsync(UpdateVendorCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _mediator.Send(command, cancellationToken);
                if (result == null)
                {
                    throw new VendorNotFoundException("Vendor not found for update.");
                }
                return result;
            }
            catch (VendorNotFoundException)
            {
                throw; // rethrow the custom exception
            }
            catch (Exception ex)
            {
                throw new SomethingElseException(ex.Message);
            }
        }

        public async Task<bool> DeleteVendorAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var command = new DeleteVendorCommand { Id = id };
                var result = await _mediator.Send(command, cancellationToken);
                if (!result)
                {
                    throw new VendorNotFoundException("Vendor not found for deletion.");
                }
                return result;
            }
            catch (VendorNotFoundException)
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
