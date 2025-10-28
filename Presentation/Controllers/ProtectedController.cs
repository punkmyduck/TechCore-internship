using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProtectedController : ControllerBase
    {
        public ProtectedController()
        {
            
        }

        [HttpGet("restricted")]
        [Authorize(Policy = "OlderThan18")]
        public async Task<IActionResult> GetRestrictedContent()
        {
            return Ok(new { Message = "Access granted : older than 18" });
        }
    }
}
