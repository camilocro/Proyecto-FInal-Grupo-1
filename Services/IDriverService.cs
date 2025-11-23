using Proyecto_FInal_Grupo_1.Models.DTOS;

namespace Proyecto_FInal_Grupo_1.Services
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverResponseDto>> GetAll();
        Task<DriverResponseDto> GetOne(Guid id);
        Task<DriverResponseDto> Create(CreateDriverDto dto);
        Task<DriverResponseDto> Update(UpdateDriverDto dto, Guid id);
        Task Delete(Guid id);
    }
}