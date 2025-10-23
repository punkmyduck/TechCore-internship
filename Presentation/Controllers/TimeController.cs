using Microsoft.AspNetCore.Mvc;
using task_1135.Domain.Services;

namespace task_1135.Presentation.Controllers
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

        [HttpGet(Name = "GetTime")]
        public IActionResult Get()
        {
            var currentTime = _timeService.GetTime();
            return Ok(new { time = currentTime });
        }
    }
}
