using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using task1135.Application.Settings;

namespace task1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppSettingsTestController : ControllerBase
    {
        private readonly MySettings _settings;
        public AppSettingsTestController(IOptions<MySettings> options)
        {
            _settings = options.Value;
        }

        /// <summary>
        /// Получить атрибуты настроек из appsettings.json
        /// </summary>
        /// <returns>Значения атрибутов настроек</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                _settings.ApiKey,
                _settings.MaxItems
            });
        }
    }
}
