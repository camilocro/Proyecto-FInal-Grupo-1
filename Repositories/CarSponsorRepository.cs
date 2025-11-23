using Microsoft.EntityFrameworkCore;
using Proyecto_FInal_Grupo_1.Data;
using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public class CarSponsorRepository : ICarSponsorRepository
    {
        private readonly AppDbContext _db;
        public CarSponsorRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(CarSponsor carSponsor)
        {
            await _db.CarSponsors.AddAsync(carSponsor);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarSponsor>> GetAll()
        {
            return await _db.CarSponsors
                .Include(cs => cs.TeamCar)
                .Include(cs => cs.Sponsor)
                .ToListAsync();
        }

        public async Task<CarSponsor?> GetOne(Guid id)
        {
            return await _db.CarSponsors
                .Include(cs => cs.TeamCar)
                .Include(cs => cs.Sponsor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Update(CarSponsor carSponsor)
        {
            _db.CarSponsors.Update(carSponsor);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(CarSponsor carSponsor)
        {
            _db.CarSponsors.Remove(carSponsor);
            await _db.SaveChangesAsync();
        }
    }
}