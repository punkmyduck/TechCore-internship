using Domain.Models;

namespace Domain.Repositories
{
    public interface IProductReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetAllAsync();
        Task<ProductReview?> GetByIdAsync(string id);
        Task<IEnumerable<ProductReview>> GetReviewsForProductByIdAsync(string productId);
        Task AddAsync(ProductReview productReview);
        Task UpdateAsync(ProductReview productReview);
        Task DeleteAsync(string id);
    }
}
