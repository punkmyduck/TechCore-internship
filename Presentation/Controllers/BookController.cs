using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;
using task_1135.Presentation.DTOs;

namespace task_1135.Presentation.Controllers
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

        [HttpGet]
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
            return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] UpdateBookDto updateBookDto)
        {
            var book = await _bookService.UpdateAsync(id, updateBookDto);
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
