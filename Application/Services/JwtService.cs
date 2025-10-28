using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using task_1135.Domain.Services;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace task_1135.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfigurationSection _jwtOptions;
        public JwtService(IConfiguration config)
        {
            _jwtOptions = config.GetSection("Jwt");
        }
        public string GenerateToken(IdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions["Issuer"],
                audience: _jwtOptions["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
