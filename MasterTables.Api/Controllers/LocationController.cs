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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var locations = await _locationService.GetAllLocationsAsync();
                return Ok(locations);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving locations.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(Guid id)
        {
            try
            {
                var location = await _locationService.GetLocationByIdAsync(id);
                return Ok(location);
            }
            catch (LocationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the location.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationCommand command)
        {
            try
            {
                var location = await _locationService.CreateLocationAsync(command);
                return CreatedAtAction(nameof(GetLocationById), new { id = location.Id }, location);
            }
            catch (LocationAlreadyExistsException)
            {
                return Conflict("Location already exists.");
            }
            catch (Exception ex)
            {
                // Log exception here
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the location.");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateLocation(Guid id, [FromBody] UpdateLocationCommand command)
        {
            try
            {
                command.Id = id;
                var location = await _locationService.UpdateLocationAsync(command);
                return Ok(location);
            }
            catch (LocationNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteLocation(Guid id)
        {
            try
            {
                var result = await _locationService.DeleteLocationAsync(id);
                return result ? NoContent() : NotFound();
            }
            catch (LocationNotFoundException)
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
