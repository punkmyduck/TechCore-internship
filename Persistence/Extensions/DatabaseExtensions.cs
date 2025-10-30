using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Persistence.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Persistence.Extensions
{
    public static class DatabaseExtensions
    {
        public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<BookContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<ApplicationIdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<BookContext>()
                .AddDefaultTokenProviders();
        }
    }
}
