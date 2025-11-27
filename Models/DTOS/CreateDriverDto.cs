using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models.DTOS
{
    public record CreateDriverDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public int Number { get; set; }
        [Required]
        public string Nationality { get; set; } = string.Empty;
        [Required]
        public Guid TeamCarId { get; set; }
        [Required]
        public Guid SponsorId { get; set; }
    }
}