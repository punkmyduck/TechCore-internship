using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using task1135.Application.Settings;

namespace task1135.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly MySettings _settings;
        private readonly ITimeService _timeService;
        public TestController(
            IOptions<MySettings> options,
            ITimeService timeService)
        {
            _settings = options.Value;
            _timeService = timeService;
        }

        /// <summary>
        /// Получить атрибуты настроек из appsettings.json
        /// </summary>
        /// <returns>Значения атрибутов настроек</returns>
        [HttpGet("SomeAppSettings")]
        public IActionResult GetSettings()
        {
            return Ok(new
            {
                _settings.ApiKey,
                _settings.MaxItems
            });
        }

        /// <summary>
        /// Получить приветствие
        /// </summary>
        /// <returns>Строка "Hello, World!"</returns>
        [HttpGet("HelloWorld")]
        public IActionResult GetHello()
        {
            return Ok(new { message = "Hello, World!" });
        }

        /// <summary>
        /// Получить текущее время
        /// </summary>
        /// <returns>Текущее время</returns>
        [HttpGet("Time")]
        public IActionResult GetTime()
        {
            var currentTime = _timeService.GetTime();
            return Ok(new { time = currentTime });
        }

        /// <summary>
        /// Получить доступ к сервису, который защищен политикой "OlderThan18"
        /// </summary>
        /// <returns>Ok, если пользователю больше 18 лет</returns>
        [HttpGet("restricted")]
        [Authorize(Policy = "OlderThan18")]
        public IActionResult GetRestrictedContent()
        {
            return Ok(new { Message = "Access granted : older than 18" });
        }

        /// <summary>
        /// Получить информацию о текущем пользователе
        /// </summary>
        /// <returns>Id и Username текущего пользователя</returns>
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetCurrentUser()
        {
            var userName = User.Identity?.Name;
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new
            {
                Id = userId,
                UserName = userName,
            });
        }
    }
}
