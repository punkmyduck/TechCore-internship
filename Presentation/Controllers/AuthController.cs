using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using task_1135.Application.DTOs;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            UserManager<IdentityUser> userManager, 
            ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserDto createUserDto)
        {
            var user = new IdentityUser { UserName = createUserDto.UserName };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            _logger.LogInformation($"user registered successfully\n\tusername : {{{user.UserName}}}\n\tpassword hash : {{{user.PasswordHash}}}");

            if (result.Succeeded)
            {
                return Ok(new { Message = "User registered successfully" });
            }

            return BadRequest(result.Errors);
        }
    }
}
