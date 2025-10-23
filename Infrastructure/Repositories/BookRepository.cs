using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Infrastructure.Storage;

namespace task_1135.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> _books;
        public BookRepository(BookStorage bookStorage)
        {
            _books = bookStorage.Books;
        }
        public Task Add(Book book)
        {
            _books.Add(book);
            return Task.CompletedTask;
        }

        public Task DeleteById(int id)
        {
            _books.RemoveAll(b => b.Id == id);
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

        public Task Update(Book book)
        {
            return Task.CompletedTask;
        }
    }
}
