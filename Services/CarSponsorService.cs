using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Repositories;

namespace Proyecto_FInal_Grupo_1.Services
{
    public class CarSponsorService : ICarSponsorService
    {
        private readonly ICarSponsorRepository _repo;

        public CarSponsorService(ICarSponsorRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<CarSponsor>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<CarSponsor> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<CarSponsor> Create(CreateCarSponsorDto dto)
        {
            var carSponsor = new CarSponsor
            {
                Id = Guid.NewGuid(),
                TeamCarId = dto.TeamCarId,
                SponsorId = dto.SponsorId,
                Location = dto.Location
            };
            await _repo.Add(carSponsor);
            return carSponsor;
        }

        public async Task<CarSponsor> Update(UpdateCarSponsorDto dto, Guid id)
        {
            var carSponsor = await _repo.GetOne(id);
            if (carSponsor == null) throw new Exception("CarSponsor relation not found");

            carSponsor.Location = dto.Location;

            await _repo.Update(carSponsor);
            return carSponsor;
        }

        public async Task Delete(Guid id)
        {
            var carSponsor = await _repo.GetOne(id);
            if (carSponsor != null)
            {
                await _repo.Delete(carSponsor);
            }
        }
    }
}
