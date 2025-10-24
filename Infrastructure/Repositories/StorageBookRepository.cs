using Microsoft.Extensions.Options;
using task_1135.Application.Settings;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Infrastructure.Storage;

namespace task_1135.Infrastructure.Repositories
{
    public class StorageBookRepository : IBookRepository
    {
        private static int _id = 0;
        private List<Book> _books;
        private AsyncSettings _settings;
        public StorageBookRepository(
            BookStorage bookStorage,
            IOptions<AsyncSettings> options)
        {
            _books = bookStorage.Books;
            _settings = options.Value;
        }
        public Task Add(Book book)
        {
            book.Id = _id++;
            _books.Add(book);
            Task.Delay(_settings.AsyncDelayInMilliseconds).Wait();
            return Task.CompletedTask;
        }

        public Task DeleteById(int id)
        {
            _books.RemoveAll(b => b.Id == id);
            Task.Delay(_settings.AsyncDelayInMilliseconds).Wait();
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Book>> GetAll()
        {
            return Task.FromResult(_books.AsEnumerable());
        }

        public Task<Book?> GetById(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            return Task.FromResult(book);
        }

        public async Task Update(int id, Book updatedBook)
        {
            var book = await GetById(id);
            if (book == null) throw new InvalidOperationException("Book with this id not found");

            book.Title = updatedBook.Title;
            book.AuthorId = updatedBook.AuthorId;
            book.YearPublished = updatedBook.YearPublished;

            Task.Delay(_settings.AsyncDelayInMilliseconds).Wait();
        }
    }
}
