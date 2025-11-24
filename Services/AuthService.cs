using Microsoft.IdentityModel.Tokens;
using Proyecto_FInal_Grupo_1.Models;
using Proyecto_FInal_Grupo_1.Models.DTOS;
using Proyecto_FInal_Grupo_1.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Proyecto_FInal_Grupo_1.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto?> Login(LoginDto dto)
        {
            var user = await _repository.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                return null;
            }

            return await GenerateAuthResponse(user);
        }

        public async Task<UserDto?> Register(RegisterDto dto)
        {
            var existing = await _repository.GetByEmail(dto.Email);
            if (existing != null) return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role
            };

            await _repository.Create(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }

        public async Task<LoginResponseDto?> RefreshToken(RefreshRequestDto dto)
        {
            var user = await _repository.GetByRefreshToken(dto.RefreshToken);
            if (user == null || user.RefreshTokenExpiresAt < DateTime.UtcNow)
            {
                return null;
            }

            return await GenerateAuthResponse(user);
        }

        private async Task<LoginResponseDto> GenerateAuthResponse(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT_KEY"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(_configuration["JWT_EXPIRE_MINUTES"] ?? "60")),
                Issuer = _configuration["JWT_ISSUER"],
                Audience = _configuration["JWT_AUDIENCE"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            await _repository.Update(user);

            return new LoginResponseDto
            {
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                },
                Token = accessToken,
                RefreshToken = refreshToken,
                Expiration = tokenDescriptor.Expires.Value
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}