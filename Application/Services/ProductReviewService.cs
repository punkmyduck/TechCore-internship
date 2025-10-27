using task_1135.Application.DTOs;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;
using task_1135.Domain.Services;

namespace task_1135.Application.Services
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository;
        public ProductReviewService(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }
        public async Task<ProductReview> AddAsync(CreateReviewDto productReview)
        {
            ProductReview review = new ProductReview
            {
                ProductId = productReview.ProductId,
                Rating = productReview.Rating,
                Comment = productReview.Comment
            };

            await _productReviewRepository.AddAsync(review);
            return review;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            if (await _productReviewRepository.GetByIdAsync(id) == null)
            {
                return false;
            }

            await _productReviewRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<ProductReview>> GetAllAsync()
        {
            return await _productReviewRepository.GetAllAsync();
        }

        public async Task<ProductReview?> GetByIdAsync(string id)
        {
            return await _productReviewRepository.GetByIdAsync(id);
        }

        public async Task<ProductReview?> UpdateAsync(string id, UpdateReviewDto productReview)
        {
            var review = await _productReviewRepository.GetByIdAsync(id);
            if (review == null) return null;

            review.Rating = productReview.Rating;
            review.Comment = productReview.Comment;

            await _productReviewRepository.UpdateAsync(review);
            return review;
        }
    }
}
