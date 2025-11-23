using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;

namespace Proyecto_FInal_Grupo_1.Services
{
    public interface ICarSponsorService
    {
        Task<IEnumerable<CarSponsor>> GetAll();
        Task<CarSponsor> GetOne(Guid id);
        Task<CarSponsor> Create(CreateCarSponsorDto dto);
        Task<CarSponsor> Update(UpdateCarSponsorDto dto, Guid id);
        Task Delete(Guid id);
    }
}