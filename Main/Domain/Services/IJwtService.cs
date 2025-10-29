using Microsoft.AspNetCore.Identity;
using task_1135.Domain.Models;

namespace task_1135.Domain.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(ApplicationIdentityUser user);
    }
}
