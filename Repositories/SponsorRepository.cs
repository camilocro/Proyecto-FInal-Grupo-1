using Microsoft.EntityFrameworkCore;
using Proyecto_FInal_Grupo_1.Data;
using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public class SponsorRepository : ISponsorRepository
    {
        private readonly AppDbContext _db;
        public SponsorRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Sponsor>> GetAll()
        {
            return await _db.Sponsors
                .Include(s => s.Drivers)         
                .Include(s => s.CarSponsors)     
                .ThenInclude(cs => cs.TeamCar)    
                .ToListAsync();
        }
        public async Task<Sponsor?> GetOne(Guid id)
        {
            return await _db.Sponsors
                .Include(s => s.Drivers)
                .Include(s => s.CarSponsors)
                .ThenInclude(cs => cs.TeamCar)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task Add(Sponsor sponsor)
        {
            await _db.Sponsors.AddAsync(sponsor);
            await _db.SaveChangesAsync();
        }
        public async Task Update(Sponsor sponsor)
        {
            _db.Sponsors.Update(sponsor);
            await _db.SaveChangesAsync();
        }
        public async Task Delete(Sponsor sponsor)
        {
            _db.Sponsors.Remove(sponsor);
            await _db.SaveChangesAsync();
        }
    }
}