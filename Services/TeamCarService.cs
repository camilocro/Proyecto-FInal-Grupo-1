using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Repositories;

namespace Proyecto_FInal_Grupo_1.Services
{
    public class TeamCarService : ITeamCarService
    {
        private readonly ITeamCarRepository _repo;

        public TeamCarService(ITeamCarRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TeamCar>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<TeamCar> GetOne(Guid id)
        {
            return await _repo.GetOne(id);
        }

        public async Task<TeamCar> Create(CreateTeamCarDto dto)
        {
            var car = new TeamCar
            {
                Id = Guid.NewGuid(),
                Model = dto.Model,
                TeamName = dto.TeamName,
                Engine = dto.Engine,
                Year = dto.Year
            };
            await _repo.Add(car);
            return car;
        }

        public async Task<TeamCar> Update(UpdateTeamCarDto dto, Guid id)
        {
            var car = await _repo.GetOne(id);
            if (car == null) throw new Exception("Team Car not found");

            car.Model = dto.Model;
            car.TeamName = dto.TeamName;
            car.Engine = dto.Engine;
            car.Year = dto.Year;

            await _repo.Update(car);
            return car;
        }

        public async Task Delete(Guid id)
        {
            var car = await _repo.GetOne(id);
            if (car != null)
            {
                await _repo.Delete(car);
            }
        }
    }
}