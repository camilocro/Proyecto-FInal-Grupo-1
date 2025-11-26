using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models
{
    public class Driver
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public int Number { get; set; }
        [Required]
        public string Nationality { get; set; } = string.Empty;

        public Guid? TeamCarId { get; set; }
        public Guid? SponsorId { get; set; }
        public TeamCar? TeamCar { get; set; }
        public Sponsor? Sponsor { get; set; }
    }
}
