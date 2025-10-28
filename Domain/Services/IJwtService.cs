using Microsoft.AspNetCore.Identity;

namespace task_1135.Domain.Services
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
