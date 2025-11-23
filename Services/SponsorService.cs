using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Repositories;


namespace Proyecto_FInal_Grupo_1.Services
{
    public class SponsorService : ISponsorService
    {
        private readonly ISponsorRepository _repo;

        public SponsorService(ISponsorRepository repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<SponsorResponseDto>> GetAll()
        {
            var sponsors = await _repo.GetAll();

            return sponsors.Select(s => new SponsorResponseDto
            {
                Id = s.Id,
                Name = s.Name,
                Industry = s.Industry,
                Amount = s.Amount
            }).ToList();
        }
        public async Task<SponsorResponseDto> GetOne(Guid id)
        {
            var sponsor = await _repo.GetOne(id);
            if (sponsor == null) return null;
            return new SponsorResponseDto
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                Industry = sponsor.Industry,
                Amount = sponsor.Amount
            };
        }
        public async Task<SponsorResponseDto> Create(CreateSponsorDto dto)
        {
            var sponsor = new Sponsor
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Industry = dto.Industry,
                Amount = dto.Amount
            };

            await _repo.Add(sponsor);

            return new SponsorResponseDto
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                Industry = sponsor.Industry,
                Amount = sponsor.Amount
            };
        }
        public async Task<SponsorResponseDto> Update(UpdateSponsorDto dto, Guid id)
        {
            var sponsor = await _repo.GetOne(id);
            if (sponsor == null)
            {
                throw new Exception($"Sponsor con ID {id} no encontrado.");
            }
            sponsor.Name = dto.Name;
            sponsor.Industry = dto.Industry;
            sponsor.Amount = dto.Amount;

            await _repo.Update(sponsor);

            return new SponsorResponseDto
            {
                Id = sponsor.Id,
                Name = sponsor.Name,
                Industry = sponsor.Industry,
                Amount = sponsor.Amount
            };
        }
        public async Task Delete(Guid id)
        {
            var sponsor = await _repo.GetOne(id);
            if (sponsor != null)
            {
                await _repo.Delete(sponsor);
            }
        }
    }
}