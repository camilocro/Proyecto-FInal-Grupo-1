using Proyecto_FInal_Grupo_1.Models;

namespace Proyecto_FInal_Grupo_1.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByRefreshToken(string refreshToken);
        Task<User> Create(User user);
        Task Update(User user);
    }
}