using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;
using task_1135.Presentation.DTOs;

namespace task_1135.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<Book> AddAsync(CreateBookDto createBookDto)
        {
            var book = new Book(createBookDto.Title, createBookDto.Author, createBookDto.YearPublished);
            await _bookRepository.Add(book);
            return book;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null) return false;
            await _bookRepository.DeleteById(id);
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _bookRepository.GetAll();
            return books;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetById(id);
            return book;
        }

        public async Task<Book?> UpdateAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetById(id);
            if (book == null) return null;
            book.UpdateDetails(updateBookDto.Title, updateBookDto.Author, updateBookDto.YearPublished);

            //ничего не делает, просто демонстрационная заглушка, поскольку обновляется ссылочное значение уже в контроллере
            await _bookRepository.Update(book);

            return book;
        }
    }
}
