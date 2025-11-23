using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public interface ISponsorRepository
    {
        Task<IEnumerable<Sponsor>> GetAll();
        Task<Sponsor?> GetOne(Guid id);
        Task Add(Sponsor sponsor);
        Task Update(Sponsor sponsor);
        Task Delete(Sponsor sponsor);
    }
}