using Domain.Models;
using Domain.DTOs;

namespace Domain.Services
{
    public interface IAuthorService
    {
        Task<Author> AddAsync(CreateAuthorDto createAuthorDto);
        Task<bool> DeleteByIdAsync(int id);
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
    }
}
