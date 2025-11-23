using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public interface IDriverRepository
    {
        Task<IEnumerable<Driver>> GetAll();
        Task<Driver?> GetOne(Guid id);
        Task Add(Driver driver);
        Task Update(Driver driver);
        Task Delete(Driver driver);
    }
}