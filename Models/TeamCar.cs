using System.ComponentModel.DataAnnotations;

namespace Proyecto_FInal_Grupo_1.Models
{
    public class TeamCar
    {
        public Guid Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string TeamName { get; set; }
        public string Engine { get; set; }
        public int Year { get; set; }

        public Driver? Driver { get; set; }
        public ICollection<CarSponsor>? CarSponsors { get; set; }
    }
}