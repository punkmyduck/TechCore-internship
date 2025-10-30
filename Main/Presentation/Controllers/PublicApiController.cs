using Microsoft.AspNetCore.Mvc;
using Domain.Services;

namespace task1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublicApiController : ControllerBase
    {
        private readonly IJsonPlaceholderService _jsonPlaceholderService;
        public PublicApiController(IJsonPlaceholderService jsonPlaceholderService)
        {
            _jsonPlaceholderService = jsonPlaceholderService;
        }

        [HttpGet("todos/1")]
        public async Task<IActionResult> GetTodosJson()
        {
            var result = await _jsonPlaceholderService.GetTodosJson();
            return Ok(result);
        }
    }
}
