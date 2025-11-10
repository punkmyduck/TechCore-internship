using BookService.Application.Settings;

namespace BookService.Extensions
{
    public static class AppSettingsExtensions
    {
        public static void AddAppSettingsConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<MySettings>(builder.Configuration.GetSection("MySettings"));
            builder.Services.Configure<AsyncSettings>(builder.Configuration.GetSection("AsyncSettings"));
        }
    }
}
