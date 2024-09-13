using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MasterTables.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;

        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaxes()
        {
            try
            {
                var taxes = await _taxService.GetAllTaxesAsync();
                return Ok(taxes);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving taxes.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaxById(Guid id)
        {
            try
            {
                var tax = await _taxService.GetTaxByIdAsync(id);
                return Ok(tax);
            }
            catch (TaxNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the tax.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTax([FromBody] TaxDto taxDto)
        {
            try
            {
                var tax = await _taxService.CreateTaxAsync(taxDto);
                return CreatedAtAction(nameof(GetTaxById), new { id = tax.Id }, tax);
            }
            catch (TaxAlreadyExistsException)
            {
                return Conflict("Tax already exists.");
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the tax.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTax(Guid id, [FromBody] UpdateTaxCommand command)
        {
            try
            {
                command.Id = id;
                var tax = await _taxService.UpdateTaxAsync(command);
                return Ok(tax);
            }
            catch (TaxNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTax(Guid id)
        {
            try
            {
                var result = await _taxService.DeleteTaxAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (TaxNotFoundException)
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
