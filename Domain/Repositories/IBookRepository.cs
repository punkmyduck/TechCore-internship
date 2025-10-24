using task_1135.Domain.Models;

namespace task_1135.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task AddAsync(Book book);
        Task UpdateAsync(int id, Book updatedBook);
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
