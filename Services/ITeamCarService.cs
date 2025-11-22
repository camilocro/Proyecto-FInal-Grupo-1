using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;

namespace Proyecto_FInal_Grupo_1.Services
{
    public interface ITeamCarService
    {
        Task<IEnumerable<TeamCar>> GetAll();
        Task<TeamCar> GetOne(Guid id);
        Task<TeamCar> Create(CreateTeamCarDto dto);
        Task<TeamCar> Update(UpdateTeamCarDto dto, Guid id);
        Task Delete(Guid id);
    }
}
