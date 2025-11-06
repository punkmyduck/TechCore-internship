using Domain.DTOs;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var book = await _bookService.AddAsync(createBookDto);
            return CreatedAtAction(nameof(AddBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var book = await _bookService.UpdateAsync(id, updateBookDto);
            return Ok(book);
        }

        [HttpPost("{bookId}/authors/{authorId}")]
        public async Task<IActionResult> AddAuthorToBook(int bookId, int authorId)
        {
            await _bookService.AddAuthorToBookAsync(bookId, authorId);
            return Ok(new { message = "Author added to book" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        [HttpGet("products/{id}/details")]
        public async Task<IActionResult> GetDetails([FromRoute] int id)
        {
            return Ok(await _bookService.GetDetailsAsync(id));
        }
    }
}
