using Domain.Models;

namespace Domain.Repositories
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
