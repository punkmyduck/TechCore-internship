using task_1135.Application.DTOs;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;

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
            var book = new Book
            {
                Title = createBookDto.Title,
                YearPublished = createBookDto.YearPublished
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            return book;
        }

        public async Task AddAuthorToBookAsync(int bookId, int authorId)
        {
            await _bookRepository.AddBookAuthorAsync(bookId, authorId);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            await _bookRepository.DeleteByIdAsync(id);
            await _bookRepository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books;
        }

        public async Task<GetBookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return GetBookDto(book);
        }

        public async Task<GetBookDto?> UpdateAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            var updatedBook = new Book
            {
                Title = updateBookDto.Title,
                YearPublished = updateBookDto.YearPublished
            };

            await _bookRepository.UpdateAsync(id, updatedBook);
            await _bookRepository.SaveChangesAsync();

            return GetBookDto(book);
        }

        private GetBookDto GetBookDto(Book book)
        {
            return new GetBookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearPublished = book.YearPublished,
                Authors = book.Authors.Select(a => new GetAuthorShortDto
                {
                    Id = a.Id,
                    Name = a.Name,
                }).ToList()
            };
        }
    }
}
