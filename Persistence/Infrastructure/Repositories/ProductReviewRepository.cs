using MongoDB.Driver;
using Domain.Models;
using Domain.Repositories;

namespace Persistence.Infrastructure.Repositories
{
    public class ProductReviewRepository : IProductReviewRepository
    {
        private readonly IMongoCollection<ProductReview> _collection;
        public ProductReviewRepository(IMongoClient client)
        {
            var database = client.GetDatabase("booksdb");
            _collection = database.GetCollection<ProductReview>("reviews");
        }
        public async Task AddAsync(ProductReview productReview)
        {
            await _collection.InsertOneAsync(productReview);
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<ProductReview>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<ProductReview?> GetByIdAsync(string id)
        {
            return await _collection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductReview>> GetReviewsForProductByIdAsync(string productId)
        {
            return await _collection.Find(r => r.ProductId == productId).ToListAsync();
        }

        public async Task UpdateAsync(ProductReview productReview)
        {
            await _collection.ReplaceOneAsync(r => r.Id == productReview.Id, productReview);
        }
    }
}
