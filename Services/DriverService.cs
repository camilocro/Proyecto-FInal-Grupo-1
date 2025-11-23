using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Repositories;

namespace Proyecto_FInal_Grupo_1.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _repo;

        public DriverService(IDriverRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<DriverResponseDto>> GetAll()
        {
            var drivers = await _repo.GetAll();
            return drivers.Select(d => new DriverResponseDto
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Number = d.Number,
                Nationality = d.Nationality,
                TeamCarId = d.TeamCarId,
                SponsorId = d.SponsorId
            });
        }

        public async Task<DriverResponseDto> GetOne(Guid id)
        {
            var driver = await _repo.GetOne(id);
            if (driver == null) return null;

            return new DriverResponseDto
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Number = driver.Number,
                Nationality = driver.Nationality,
                TeamCarId = driver.TeamCarId,
                SponsorId = driver.SponsorId
            };
        }

        public async Task<DriverResponseDto> Create(CreateDriverDto dto)
        {
            var driver = new Driver
            {
                Id = Guid.NewGuid(),
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Number = dto.Number,
                Nationality = dto.Nationality,
                TeamCarId = dto.TeamCarId,
                SponsorId = dto.SponsorId
            };

            await _repo.Add(driver);

            return new DriverResponseDto
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Number = driver.Number,
                Nationality = driver.Nationality,
                TeamCarId = driver.TeamCarId,
                SponsorId = driver.SponsorId
            };
        }

        public async Task<DriverResponseDto> Update(UpdateDriverDto dto, Guid id)
        {
            var driver = await _repo.GetOne(id);
            if (driver == null) throw new Exception("Driver not found");

            driver.FirstName = dto.FirstName;
            driver.LastName = dto.LastName;
            driver.Number = dto.Number;
            driver.Nationality = dto.Nationality;
            driver.TeamCarId = dto.TeamCarId;
            driver.SponsorId = dto.SponsorId;

            await _repo.Update(driver);

            return new DriverResponseDto
            {
                Id = driver.Id,
                FirstName = driver.FirstName,
                LastName = driver.LastName,
                Number = driver.Number,
                Nationality = driver.Nationality,
                TeamCarId = driver.TeamCarId,
                SponsorId = driver.SponsorId
            };
        }

        public async Task Delete(Guid id)
        {
            var driver = await _repo.GetOne(id);
            if (driver != null)
            {
                await _repo.Delete(driver);
            }
        }
    }
}