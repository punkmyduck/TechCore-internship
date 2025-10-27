using task_1135.Application.DTOs;
using task_1135.Domain.Models;

namespace task_1135.Domain.Services
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ProductReview>> GetAllAsync();
        Task<ProductReview?> GetByIdAsync(string id);
        Task<ProductReview> AddAsync(CreateReviewDto productReview);
        Task<ProductReview?> UpdateAsync(UpdateReviewDto productReview);
        Task<bool> DeleteAsync(string id);
    }
}
