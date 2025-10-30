using Microsoft.AspNetCore.Identity;
using Domain.Models;

namespace Domain.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationIdentityUser user);
    }
}
