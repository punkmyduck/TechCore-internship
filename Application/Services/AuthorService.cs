using task_1135.Application.DTOs;
using task_1135.Domain.Models;
using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class AuthorService : IAuthorService
    {
        public Task<Author> AddAsync(CreateAuthorDto createAuthorDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Author?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
