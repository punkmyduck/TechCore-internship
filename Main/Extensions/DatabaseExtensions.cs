using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Persistence.Infrastructure;

namespace task1135.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            if (builder.Environment.EnvironmentName == "IntegrationTests")
            {
                builder.Services.AddDbContext<BookContext>(options =>
                    options.UseInMemoryDatabase("TestDB"));
            }
            else
            {
                builder.Services.AddDbContext<BookContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookContext>()
                .AddDefaultTokenProviders();
        }
    }
}
