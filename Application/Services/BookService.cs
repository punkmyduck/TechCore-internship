using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using task_1135.Application.DTOs;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly IDistributedCache _distributedCache;
        public BookService(
            IBookRepository bookRepository,
            IDistributedCache distributedCache,
            IProductReviewRepository productReviewRepository)
        {
            _bookRepository = bookRepository;
            _distributedCache = distributedCache;
            _productReviewRepository = productReviewRepository;
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
            await _distributedCache.RemoveAsync($"book:{bookId}");

            await _bookRepository.AddBookAuthorAsync(bookId, authorId);
            await _bookRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            await ClearCache(id);

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
            string cacheKey = $"book:{id}";
            var cached = await _distributedCache.GetStringAsync(cacheKey);
            if (cached != null) return JsonSerializer.Deserialize<GetBookDto>(cached);

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            var bookDto = GetBookDto(book);

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(bookDto));
            return bookDto;
        }

        public async Task<ProductDetailsDto> GetDetailsAsync(int id)
        {
            string cacheKey = $"bookdetails:{id}";
            var cached = await _distributedCache.GetStringAsync(cacheKey);
            if (cached != null) return JsonSerializer.Deserialize<ProductDetailsDto>(cached)!;

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new KeyNotFoundException($"Book with id = {id} not found");

            var reviews = await _productReviewRepository.GetReviewsForProductByIdAsync(id.ToString());
            var authors = GetAuthorsShortDto(book);

            var bookDetails = new ProductDetailsDto
            {
                Id = book.Id,
                Title = book.Title,
                YearPublished = book.YearPublished,
                Reviews = reviews.ToList(),
                Authors = authors
            };

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(bookDetails));

            return bookDetails;
        }

        public async Task<GetBookDto?> UpdateAsync(int id, UpdateBookDto updateBookDto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            await ClearCache(id);

            var updatedBook = new Book
            {
                Title = updateBookDto.Title,
                YearPublished = updateBookDto.YearPublished
            };

            await _bookRepository.UpdateAsync(id, updatedBook);
            await _bookRepository.SaveChangesAsync();

            return GetBookDto(book);
        }

        private async Task ClearCache(int id)
        {
            await _distributedCache.RemoveAsync($"book:{id}");
            await _distributedCache.RemoveAsync($"bookdetails:{id}");
        }

        private GetBookDto GetBookDto(Book book)
        {
            return new GetBookDto
            {
                Id = book.Id,
                Title = book.Title,
                YearPublished = book.YearPublished,
                Authors = GetAuthorsShortDto(book)
            };
        }

        private List<GetAuthorShortDto> GetAuthorsShortDto(Book book)
        {
            return book.Authors.Select(a => new GetAuthorShortDto
            {
                Id = a.Id,
                Name = a.Name,
            }).ToList();
        }
    }
}
