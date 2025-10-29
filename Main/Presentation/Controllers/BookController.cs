using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using task_1135.Application.DTOs;
using task_1135.Domain.Services;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// Получить список всех книг
        /// </summary>
        /// <returns>Список книг</returns>
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        /// <summary>
        /// Получить книгу по указанному id
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>Книга с указанным id или NotFound() если книги с указанным id не существует</returns>
        [HttpGet("{id}")]
        [OutputCache(PolicyName = "BookPolicy")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        /// <summary>
        /// Добавить книгу в библиотеку
        /// </summary>
        /// <param name="createBookDto">Запись с атрибутами книги</param>
        /// <returns>Информация о добавленной книге</returns>
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var book = await _bookService.AddAsync(createBookDto);
            return CreatedAtAction(nameof(AddBook), new {id = book.Id}, book);
        }

        /// <summary>
        /// Изменить существующую книгу
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <param name="updateBookDto">Запись с атрибутами книги</param>
        /// <returns>Информация о измененной книге</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var book = await _bookService.UpdateAsync(id, updateBookDto);
            return Ok(book);
        }

        /// <summary>
        /// Добавить автора для книги
        /// </summary>
        /// <param name="bookId">Идентификатор книги</param>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Сообщение об успешном добавлении к книге автора</returns>
        [HttpPost("{bookId}/authors/{authorId}")]
        public async Task<IActionResult> AddAuthorToBook(int bookId, int authorId)
        {
            await _bookService.AddAuthorToBookAsync(bookId, authorId);
            return Ok(new { message = "Author added to book" });
        }

        /// <summary>
        /// Удалить книгу по указанному id
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>NotFound если книги с указанным id не существует или NoContent, если книга успешно удалена</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        /// <summary>
        /// Получить детальную информацию о книге по указанному id
        /// </summary>
        /// <param name="id">Идентификатор книги</param>
        /// <returns>Информация о книге, авторах и отзывах к этой книге</returns>
        [HttpGet("products/{id}/details")]
        public async Task<IActionResult> GetDetails([FromRoute] int id)
        {
            return Ok(await _bookService.GetDetailsAsync(id));
        }
    }
}
