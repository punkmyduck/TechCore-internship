using Domain.Models;
using Domain.DTOs;

namespace Domain.Services
{
    public interface IProductReviewService
    {
        Task<IEnumerable<ProductReview>> GetAllAsync();
        Task<ProductReview?> GetByIdAsync(string id);
        Task<ProductReview> AddAsync(CreateReviewDto productReview);
        Task<ProductReview?> UpdateAsync(string id, UpdateReviewDto productReview);
        Task<bool> DeleteAsync(string id);
    }
}
