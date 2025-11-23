using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models.DTOS
{
    public record CreateCarSponsorDto
    {
        [Required]
        public Guid TeamCarId { get; set; }
        [Required]
        public Guid SponsorId { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
