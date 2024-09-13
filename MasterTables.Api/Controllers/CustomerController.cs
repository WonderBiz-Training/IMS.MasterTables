using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Application.Commands;
using MasterTables.Application.Queries;
using MasterTables.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MasterTables.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomersAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving customers.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByIdAsync(id);
                return Ok(customer);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the customer.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var customer = await _customerService.CreateCustomerAsync(command);
                return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
            }
            catch (CustomerAlreadyExistsException)
            {
                return Conflict("Customer already exists.");
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the customer.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                command.Id = id;
                var customer = await _customerService.UpdateCustomerAsync(command);
                return Ok(customer);
            }
            catch (CustomerNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var result = await _customerService.DeleteCustomerAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (CustomerNotFoundException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
