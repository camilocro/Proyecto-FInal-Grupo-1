using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Services;

namespace Proyecto_FInal_Grupo_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DriverController : ControllerBase
    {
        private readonly IDriverService _service;
        private readonly ILogger<DriverController> _logger;

        public DriverController(IDriverService service, ILogger<DriverController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all drivers");
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var driver = await _service.GetOne(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateDriverDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var driver = await _service.Create(dto);
                return CreatedAtAction(nameof(GetOne), new { id = driver.Id }, driver);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating driver");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDriverDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var driver = await _service.Update(dto, id);
                return Ok(driver);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}