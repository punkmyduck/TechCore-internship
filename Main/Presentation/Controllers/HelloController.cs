using Microsoft.AspNetCore.Mvc;

namespace task1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        /// <summary>
        /// Получить приветствие
        /// </summary>
        /// <returns>Строка "Hello, World!"</returns>
        [HttpGet(Name = "Hello")]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello, World!" });
        }
    }
}
