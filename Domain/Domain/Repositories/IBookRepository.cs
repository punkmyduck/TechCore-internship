using Domain.Models;

namespace Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(int id, Book updatedBook);
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
        Task AddBookAuthorAsync(int bookId, int authorId);
    }
}
