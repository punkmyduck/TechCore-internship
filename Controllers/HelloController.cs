using Microsoft.AspNetCore.Mvc;

namespace task_1135.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet(Name = "Hello")]
        public IActionResult Get()
        {
            return Ok(new { message = "Hello, World!" });
        }
    }
}
