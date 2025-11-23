using Microsoft.EntityFrameworkCore;
using Proyecto_FInal_Grupo_1.Data;
using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _db;

        public DriverRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task Add(Driver driver)
        {
            await _db.Drivers.AddAsync(driver);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            return await _db.Drivers
                .Include(d => d.TeamCar)
                .Include(d => d.Sponsor)
                .ToListAsync();
        }

        public async Task<Driver?> GetOne(Guid id)
        {
            return await _db.Drivers
                .Include(d => d.TeamCar)
                .Include(d => d.Sponsor)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task Update(Driver driver)
        {
            _db.Drivers.Update(driver);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Driver driver)
        {
            _db.Drivers.Remove(driver);
            await _db.SaveChangesAsync();
        }
    }
}