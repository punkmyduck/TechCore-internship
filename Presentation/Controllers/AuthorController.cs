using Microsoft.AspNetCore.Mvc;
using task_1135.Application.DTOs;
using task_1135.Domain.Services;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _authorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            return Ok(await _authorService.AddAsync(createAuthorDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var deleted = await _authorService.DeleteByIdAsync(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}
