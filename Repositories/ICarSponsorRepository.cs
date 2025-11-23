using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public interface ICarSponsorRepository
    {
        Task<IEnumerable<CarSponsor>> GetAll();
        Task<CarSponsor?> GetOne(Guid id);
        Task Add(CarSponsor carSponsor);
        Task Update(CarSponsor carSponsor);
        Task Delete(CarSponsor carSponsor);
    }
}