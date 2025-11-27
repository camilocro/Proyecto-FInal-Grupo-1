using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models
{
    public class CarSponsor
    {
        public Guid Id { get; set; }
        [Required]
        public string Location { get; set; } = string.Empty;

        public Guid TeamCarId { get; set; }
        public Guid SponsorId { get; set; }

        public TeamCar? TeamCar { get; set; }
        public Sponsor? Sponsor { get; set; }
    }
}