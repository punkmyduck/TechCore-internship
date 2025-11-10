using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Domain.Services;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Domain.Models;

namespace BookService.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfigurationSection _jwtOptions;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        public JwtService(
            IConfiguration config,
            UserManager<ApplicationIdentityUser> userManager)
        {
            _jwtOptions = config.GetSection("Jwt");
            _userManager = userManager;
        }
        public async Task<string> GenerateTokenAsync(ApplicationIdentityUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions["SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd"))
            };
            claims.AddRange((await _userManager.GetRolesAsync(user)).Select(r => new Claim(ClaimTypes.Role, r)));

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
