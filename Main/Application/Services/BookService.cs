using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.DTOs;

namespace task1135.Application.Services
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

        public async Task<ReturnBookDto?> GetByIdAsync(int id)
        {
            string cacheKey = $"book:{id}";
            var cached = await _distributedCache.GetStringAsync(cacheKey);
            if (cached != null) return JsonSerializer.Deserialize<ReturnBookDto>(cached);

            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            var bookDto = GetReturnBookDto(book);

            await _distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(bookDto));
            return bookDto;
        }

        public async Task<ProductDetailsDto> GetDetailsAsync(int id)
        {
            ReturnBookDto bookDto;

            string cacheKey = $"book:{id}";
            var cached = await _distributedCache.GetStringAsync(cacheKey);
            if (cached != null) bookDto = JsonSerializer.Deserialize<ReturnBookDto>(cached)!;
            else
            {
                var book = await _bookRepository.GetByIdAsync(id);
                if (book == null) throw new KeyNotFoundException($"Book with id = {id} not found");
                bookDto = GetReturnBookDto(book);

                await _distributedCache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(bookDto),
                    new DistributedCacheEntryOptions 
                    { 
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1) 
                    });
            }

            var reviews = await _productReviewRepository.GetReviewsForProductByIdAsync(id.ToString());

            var cachedAverageRating = await _distributedCache.GetStringAsync($"rating:{id}");
            if (cachedAverageRating == null) throw new ArgumentNullException("Average product rating still not calculated, try again later");
            double averageRating = JsonSerializer.Deserialize<double>(cachedAverageRating);

            var bookDetails = new ProductDetailsDto
            {
                Id = bookDto.Id,
                Title = bookDto.Title,
                YearPublished = bookDto.YearPublished,
                AverageProductRating = averageRating,
                Reviews = reviews.ToList(),
                Authors = bookDto.Authors
            };

            return bookDetails;
        }

        public async Task<ReturnBookDto?> UpdateAsync(int id, UpdateBookDto updateBookDto)
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

            return GetReturnBookDto(book);
        }

        private async Task ClearCache(int id)
        {
            await _distributedCache.RemoveAsync($"book:{id}");
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
