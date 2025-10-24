using task_1135.Domain.Models;

namespace task_1135.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task AddAsync(Author author);
        Task<Author?> GetByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task DeleteByIdAsync(int id);
        Task SaveChangesAsync();
    }
}
