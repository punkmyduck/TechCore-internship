using task_1135.Domain.Models;

namespace task_1135.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetById(int id);
        Task Add(Book book);
        Task Update(Book book);
        Task DeleteById(int id);
    }
}
