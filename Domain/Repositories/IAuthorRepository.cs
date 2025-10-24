using task_1135.Domain.Models;

namespace task_1135.Domain.Repositories
{
    public interface IAuthorRepository
    {
        Task Add(Author author);
        Task<Author?> GetById(int id);
        Task<IEnumerable<Author>> GetAll();
        Task DeleteById(int id);
    }
}
