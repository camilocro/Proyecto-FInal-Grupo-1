using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models
{
    public class Sponsor
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Industry { get; set; } = string.Empty;
        public decimal Amount { get; set; }

        public ICollection<CarSponsor> CarSponsors { get; set; } = new List<CarSponsor>();
        public ICollection<Driver> Drivers { get; set; } = new List<Driver>();
    }
}