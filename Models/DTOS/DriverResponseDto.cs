namespace Proyecto_FInal_Grupo_1.Models.DTOS
{
    public class DriverResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Nationality { get; set; } = string.Empty;

        public Guid? TeamCarId { get; set; }
        public Guid? SponsorId { get; set; }
    }
}