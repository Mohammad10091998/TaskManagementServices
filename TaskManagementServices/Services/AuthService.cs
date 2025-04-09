using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementServices.Auth;
using TaskManagementServices.Domain;
using TaskManagementServices.DTOs;
using TaskManagementServices.Repositories.Interface;
using TaskManagementServices.Services.Interface;

namespace TaskManagementServices.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JwtSettings> _jwtSettings;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
            _passwordHasher = new PasswordHasher<User>();
            _logger = logger;
        }

        public async Task<string?> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
                if (user == null) throw new InvalidDataException("Wrong email or password");

                var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
                if (result != PasswordVerificationResult.Success) return null;

                return GenerateToken(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while logging with id {email}", loginDto.Email);
                throw;
            }
        }

        private string GenerateToken(User user)
        {
            var jwt = _jwtSettings.Value;

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
