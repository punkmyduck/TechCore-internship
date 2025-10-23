using task_1135.Domain.Models;
using task_1135.Presentation.DTOs;

namespace task_1135.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(CreateBookDto createBookDto);
        Task<Book?> UpdateAsync(int id, UpdateBookDto updateBookDto);
        Task<bool> DeleteAsync(int id);
    }
}
