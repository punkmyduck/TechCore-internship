using Domain.DTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;

namespace BookServiceDemo.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger<BookService> _logger;

        public BookService(
            ILogger<BookService> logger,
            IBookRepository bookRepository)
        {
            _logger = logger;
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

            _logger.LogInformation("Added new book with ID {BookId}", book.Id);

            return book;
        }

        public async Task AddAuthorToBookAsync(int bookId, int authorId)
        {
            await _bookRepository.AddBookAuthorAsync(bookId, authorId);
            await _bookRepository.SaveChangesAsync();

            _logger.LogInformation("Added author with ID {AuthorId} to book with ID {BookId}", authorId, bookId);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;
            await _bookRepository.DeleteByIdAsync(id);
            await _bookRepository.SaveChangesAsync();
            _logger.LogInformation("Deleted book with ID {BookId}", id);
            return true;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books;
        }

        public async Task<ReturnBookDto?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;
            var bookDto = GetReturnBookDto(book);
            _logger.LogInformation("Retrieved book with ID {BookId}", id);
            return bookDto;
        }

        public async Task<ProductDetailsDto> GetDetailsAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new KeyNotFoundException($"Book with id = {id} not found");
            var bookDto = GetReturnBookDto(book);
            var bookDetails = new ProductDetailsDto
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                YearPublished = bookDto.YearPublished,
                AverageProductRating = 5,
                Authors = bookDto.Authors
            };
            _logger.LogInformation("Retrieved details for book with ID {BookId}", id);
            return bookDetails;
        }

        public async Task<ReturnBookDto?> UpdateAsync(int id, UpdateBookDto updateBookDto)
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
            _logger.LogInformation("Updated book with ID {BookId}", id);
            return GetReturnBookDto(updatedBook);
        }

        private ReturnBookDto GetReturnBookDto(Book book)
        {
            return new ReturnBookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearPublished = book.YearPublished,
                Authors = GetAuthorsShortDto(book)
            };
        }

        private List<ReturnAuthorShortDto> GetAuthorsShortDto(Book book)
        {
            return book.Authors.Select(a => new ReturnAuthorShortDto
            {
                Id = a.Id,
                Name = a.Name,
            }).ToList();
        }
    }
}
