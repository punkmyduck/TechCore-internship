using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Domain.Services;

namespace BookService.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Получить всех авторов
        /// </summary>
        /// <returns>Список авторов из базы данных</returns>
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            return Ok(await _authorService.GetAllAsync());
        }

        /// <summary>
        /// Получить автора по id
        /// </summary>
        /// <param name="id">Идентификатор автора</param>
        /// <returns>Автор с указанным id или NotFound, если такого автора нет</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById([FromRoute] int id)
        {
            var author = await _authorService.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        /// <summary>
        /// Добавить автора
        /// </summary>
        /// <param name="createAuthorDto">Класс с атрибутами для добавления автора</param>
        /// <returns>Информация о добавленном авторе</returns>
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] CreateAuthorDto createAuthorDto)
        {
            return Ok(await _authorService.AddAsync(createAuthorDto));
        }

        /// <summary>
        /// Удалить автора
        /// </summary>
        /// <param name="id">Идентификатор автора к удалению</param>
        /// <returns>NoContent, если автор удален, NotFound, если автор с таким id не найден</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
        {
            var deleted = await _authorService.DeleteByIdAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
