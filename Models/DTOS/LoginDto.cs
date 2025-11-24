using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models.DTOS
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}