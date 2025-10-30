using Microsoft.AspNetCore.Mvc;
using Domain.Services;

namespace task1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TimeController : ControllerBase
    {
        private readonly ITimeService _timeService;
        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        /// <summary>
        /// Получить текущее время
        /// </summary>
        /// <returns>Текущее время</returns>
        [HttpGet(Name = "GetTime")]
        public IActionResult Get()
        {
            var currentTime = _timeService.GetTime();
            return Ok(new { time = currentTime });
        }
    }
}
