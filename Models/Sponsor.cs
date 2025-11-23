
namespace Proyecto_FInal_Grupo_1.Models
{
    public class Sponsor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
        public decimal Amount { get; set; }
        public ICollection<CarSponsor>? CarSponsors { get; set; }
        public ICollection<Driver>? Drivers { get; set; }
    }
}