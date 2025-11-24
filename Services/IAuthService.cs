using Proyecto_FInal_Grupo_1.Models.DTOS;

namespace Proyecto_FInal_Grupo_1.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> Login(LoginDto dto);
        Task<UserDto?> Register(RegisterDto dto);
        Task<LoginResponseDto?> RefreshToken(RefreshRequestDto dto);
    }
}