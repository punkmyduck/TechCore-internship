using task_1135.Domain.Models;

namespace task_1135.Domain.Repositories
{
    public interface IProductReviewRepository
    {
        Task<IEnumerable<ProductReview>> GetAllAsync();
        Task<ProductReview?> GetByIdAsync(string id);
        Task AddAsync(ProductReview productReview);
        Task UpdateAsync(ProductReview productReview);
        Task DeleteAsync(string id);
    }
}
