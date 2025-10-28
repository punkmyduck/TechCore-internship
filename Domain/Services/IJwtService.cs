using Microsoft.AspNetCore.Identity;

namespace task_1135.Domain.Services
{
    public interface IJwtService
    {
        string GenerateToken(IdentityUser user);
    }
}
