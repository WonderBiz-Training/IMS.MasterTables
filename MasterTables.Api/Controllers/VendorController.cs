using MasterTables.Application.Commands;
using MasterTables.Application.DTOs;
using MasterTables.Application.Interfaces;
using MasterTables.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MasterTables.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            try
            {
                var vendors = await _vendorService.GetAllVendorsAsync();
                return Ok(vendors);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving vendors.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendorById(Guid id)
        {
            try
            {
                var vendor = await _vendorService.GetVendorByIdAsync(id);
                return Ok(vendor);
            }
            catch (VendorNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the vendor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] VendorDto vendorDto)
        {
            try
            {
                var vendor = await _vendorService.CreateVendorAsync(vendorDto);
                return CreatedAtAction(nameof(GetVendorById), new { id = vendor.Id }, vendor);
            }
            catch (VendorAlreadyExistsException)
            {
                return Conflict("Vendor already exists.");
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the vendor.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateVendor(Guid id, [FromBody] UpdateVendorCommand command)
        {
            try
            {
                command.Id = id;
                var vendor = await _vendorService.UpdateVendorAsync(command);
                return Ok(vendor);
            }
            catch (VendorNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteVendor(Guid id)
        {
            try
            {
                var result = await _vendorService.DeleteVendorAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (VendorNotFoundException)
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
