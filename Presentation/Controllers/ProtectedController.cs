using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
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

        /// <summary>
        /// Получить доступ к сервису, который защищен политикой "OlderThan18"
        /// </summary>
        /// <returns>Ok, если пользователю больше 18 лет</returns>
        [HttpGet("restricted")]
        [Authorize(Policy = "OlderThan18")]
        public async Task<IActionResult> GetRestrictedContent()
        {
            return Ok(new { Message = "Access granted : older than 18" });
        }

        /// <summary>
        /// Получить информацию о текущем пользователе
        /// </summary>
        /// <returns>Id и Username текущего пользователя</returns>
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
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
