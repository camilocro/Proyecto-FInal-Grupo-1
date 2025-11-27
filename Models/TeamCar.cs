using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto_FInal_Grupo_1.Models
{
    public class TeamCar
    {
        public Guid Id { get; set; }

        [Required]
        public string Model { get; set; } = string.Empty;

        [Required]
        public string TeamName { get; set; } = string.Empty;

        public string Engine { get; set; } = string.Empty;

        public int Year { get; set; }

        public Driver? Driver { get; set; }

        public ICollection<CarSponsor> CarSponsors { get; set; } = new List<CarSponsor>();
    }
}