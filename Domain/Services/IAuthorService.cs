using task_1135.Application.DTOs;
using task_1135.Domain.Models;

namespace task_1135.Domain.Services
{
    public interface IAuthorService
    {
        Task<Author> AddAsync(CreateAuthorDto createAuthorDto);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
    }
}
