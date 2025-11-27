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
            _logger.LogInformation("Consultando todos los pilotos.");
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var item = await _service.GetOne(id);
            if (item == null)
            {
                _logger.LogWarning($"Piloto {id} no encontrado.");
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateDriverDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await _service.Create(dto);
                _logger.LogInformation($"Piloto creado: {item.FirstName} {item.LastName}");
                return CreatedAtAction(nameof(GetOne), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando piloto");
                return StatusCode(500, "Error interno del servidor: " + ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDriverDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await _service.Update(dto, id);
                _logger.LogInformation($"Piloto actualizado: {id}");
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error actualizando piloto {id}");
                if (ex.Message.Contains("not found")) return NotFound(ex.Message);

                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.Delete(id);
                _logger.LogInformation($"Piloto eliminado: {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error eliminando piloto {id}");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}