using task1135.Application.Settings;

namespace task1135.Extensions
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
