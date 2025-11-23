using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS; 
using Proyecto_FInal_Grupo_1.Services;

namespace Proyecto_FInal_Grupo_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _service;
        private readonly ILogger<SponsorController> _logger;

        public SponsorController(ISponsorService service, ILogger<SponsorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Obteniendo todos los patrocinadores");
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var sponsor = await _service.GetOne(id);
            if (sponsor == null)
            {
                _logger.LogWarning($"Patrocinador con ID {id} no encontrado");
                return NotFound();
            }
            return Ok(sponsor);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateSponsorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var sponsor = await _service.Create(dto);
                _logger.LogInformation($"Patrocinador creado: {sponsor.Name}");
                return CreatedAtAction(nameof(GetOne), new { id = sponsor.Id }, sponsor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando el patrocinador");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSponsorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var updatedSponsor = await _service.Update(dto, id);
                return Ok(updatedSponsor);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error actualizando patrocinador {id}: {ex.Message}");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            _logger.LogInformation($"Patrocinador {id} eliminado");
            return NoContent();
        }
    }
}