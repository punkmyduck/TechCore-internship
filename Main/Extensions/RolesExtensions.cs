using Microsoft.AspNetCore.Identity;

namespace task_1135.Extensions
{
    public static class RolesExtensions
    {
        public static async Task CreateRoleAsync(this IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(roleName))
                await roleManager.CreateAsync(new IdentityRole(roleName));
        }

        public static async Task CreateRoleAsync(this IServiceProvider serviceProvider, IEnumerable<string> rolesNames)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            foreach(var roleName in rolesNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                    await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
