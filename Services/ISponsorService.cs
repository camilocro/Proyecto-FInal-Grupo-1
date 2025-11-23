using Proyecto_FInal_Grupo_1.Models.DTOS;

namespace Proyecto_FInal_Grupo_1.Services
{
    public interface ISponsorService
    {
        Task<IEnumerable<SponsorResponseDto>> GetAll();
        Task<SponsorResponseDto> GetOne(Guid id);
        Task<SponsorResponseDto> Create(CreateSponsorDto dto);
        Task<SponsorResponseDto> Update(UpdateSponsorDto dto, Guid id);
        Task Delete(Guid id);
    }
}