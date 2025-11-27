using Microsoft.AspNetCore.Mvc;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Services;

namespace Proyecto_FInal_Grupo_1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var response = await _authService.Login(dto);
            if (response == null)
            {
                _logger.LogWarning("Intento de login fallido para: {Email}", dto.Email);
                return Unauthorized("Credenciales inválidas");
            }

            _logger.LogInformation("Usuario logueado: {Email}", dto.Email);
            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var user = await _authService.Register(dto);
                if (user == null) return BadRequest("El usuario ya existe.");

                _logger.LogInformation("Nuevo usuario registrado: {Email}", dto.Email);
                return CreatedAtAction(nameof(Login), null, user);
            }
            catch (Exception ex)
            {
                // --- CAMBIO PARA DEPURACIÓN ---
                // Aquí imprimimos el error real para verlo en Swagger
                var errorMessage = $"ERROR REAL: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $" | INNER: {ex.InnerException.Message}";
                }

                _logger.LogError(ex, "Error en registro");
                return StatusCode(500, errorMessage);
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
        {
            var response = await _authService.RefreshToken(dto);
            if (response == null) return Unauthorized("Token inválido o expirado");

            return Ok(response);
        }
    }
}