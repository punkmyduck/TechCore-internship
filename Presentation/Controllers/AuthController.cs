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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AuthController> _logger;
        public AuthController(
            UserManager<IdentityUser> userManager, 
            ILogger<AuthController> logger,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto createUserDto)
        {
            var user = new IdentityUser { UserName = createUserDto.UserName };
            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation($"user registered successfully\n\tusername : {{{user.UserName}}}\n\tpassword hash : {{{user.PasswordHash}}}");
                return Ok(new { Message = "User registered successfully" });
            }

            _logger.LogInformation($"user registration incorrect\n\terrors : {{{string.Join("}, {", result.Errors.Select(a => a.Description))}}}");
            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn([FromBody] SignInDto signInDto)
        {
            var user = await _userManager.FindByNameAsync(signInDto.UserName);

            if (user == null)
            {
                _logger.LogInformation($"Login failed: user {signInDto.UserName} not found");
                return BadRequest(new { Message = "Failure" });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, signInDto.Password, false);

            if (result.Succeeded)
            {
                _logger.LogInformation($"User {{{user.UserName}}} logged in successfully");
                return Ok(new { Message = "Success" });
            }

            _logger.LogInformation($"Failed login attempt\n\tUsername : {{{user.UserName}}}");
            return BadRequest(new { Message = "Failure" });
        }
    }
}
