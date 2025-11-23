using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Services;

namespace Proyecto_FInal_Grupo_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarSponsorController : ControllerBase
    {
        private readonly ICarSponsorService _service;
        private readonly ILogger<CarSponsorController> _logger;

        public CarSponsorController(ICarSponsorService service, ILogger<CarSponsorController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Obteniendo todas las asignaciones de sponsors");
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var item = await _service.GetOne(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Create([FromBody] CreateCarSponsorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await _service.Create(dto);
                return CreatedAtAction(nameof(GetOne), new { id = item.Id }, item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error asignando sponsor");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarSponsorDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                var item = await _service.Update(dto, id);
                return Ok(item);
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