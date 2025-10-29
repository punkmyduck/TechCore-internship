using task_1135.Application.DTOs;
using task_1135.Domain.Models;

namespace task_1135.Domain.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<ReturnBookDto?> GetByIdAsync(int id);
        Task<Book> AddAsync(CreateBookDto createBookDto);
        Task<ReturnBookDto?> UpdateAsync(int id, UpdateBookDto updateBookDto);
        Task<bool> DeleteAsync(int id);
        Task AddAuthorToBookAsync(int bookId, int authorId);
        Task<ProductDetailsDto> GetDetailsAsync(int id);
    }
}
