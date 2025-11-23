namespace Proyecto_FInal_Grupo_1.Models.DTOS
{
    public class DriverResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Number { get; set; }
        public string Nationality { get; set; }
        public Guid TeamCarId { get; set; }
        public Guid SponsorId { get; set; }
    }
}