using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Presentation.DTOs;

namespace task_1135.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookRepository.GetAll();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto createBookDto)
        {
            var book = new Book(createBookDto.Title, createBookDto.Author, createBookDto.YearPublished);
            await _bookRepository.Add(book);
            return CreatedAtAction(nameof(GetBookById), new {id = book.Id}, book);
        }

        [HttpPut]
        public IActionResult UpdateBook()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook()
        {
            return Ok();
        }
    }
}
