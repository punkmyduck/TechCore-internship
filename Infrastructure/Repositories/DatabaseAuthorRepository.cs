using task_1135.Domain.Models;
using task_1135.Domain.Repositories;

namespace task_1135.Infrastructure.Repositories
{
    public class DatabaseAuthorRepository : IAuthorRepository
    {
        public Task Add(Author author)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
