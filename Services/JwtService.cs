using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API_VehiclesAPP.Configurations;
using API_VehiclesAPP.Entities;
using API_VehiclesAPP.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API_VehiclesAPP.Services
{
    public class JwtService(IOptions<JwtConfig> options, ILogger<JwtService> logger) : IJwtService
    {
        private readonly JwtConfig _options = options.Value;
        private readonly ILogger<JwtService> _logger = logger;


        public string GenerateToken(string ID, string Email = "", string Role = "User", string Auth = "false")
        {
            JwtSecurityToken token = null!;
            try
            {
                Claim[] claims =
                [
                    new Claim(ClaimTypes.NameIdentifier, ID),
                    new Claim(ClaimTypes.Email, Email),
                    new Claim(ClaimTypes.Role, Role),
                    new Claim("Auth", Auth),
                ];

                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_options.Key));

                SigningCredentials credential = new(key, SecurityAlgorithms.HmacSha256);

                token = new(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims, 
                    expires: DateTime.UtcNow.AddMinutes(60), 
                    signingCredentials: credential);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al Generar JWT | {ex.Message}.");
                throw;
            }

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
