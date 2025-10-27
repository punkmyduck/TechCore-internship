
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using task_1135.Domain.Repositories;

namespace task_1135.Application.Services
{
    public class AverageRatingCalculatorService : BackgroundService
    {
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly IDistributedCache _distributedCache;
        private readonly ILogger<AverageRatingCalculatorService> _logger;
        public AverageRatingCalculatorService(
            IProductReviewRepository productReviewRepository,
            IDistributedCache distributedCache,
            ILogger<AverageRatingCalculatorService> logger
            )
        {
            _productReviewRepository = productReviewRepository;
            _distributedCache = distributedCache;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AverageRatingCalculatorService started");
            
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CalculateAndCacheAverageRatings(stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while calculating average ratings");
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }

            _logger.LogInformation("AverageRatingCalculatorService stopped");
        }

        private async Task CalculateAndCacheAverageRatings(CancellationToken cancellationToken)
        {
            var allReviews = await _productReviewRepository.GetAllAsync();
            var reviewsByProduct = allReviews
                .GroupBy(r => r.ProductId)
                .Select(g => new { ProductId = g.Key, AverageRating = g.Average(r => r.Rating) })
                .ToList();

            foreach (var kvp in reviewsByProduct)
            {
                string cacheKey = $"rating:{kvp.ProductId}";
                string value = JsonSerializer.Serialize(kvp.AverageRating);
                await _distributedCache.SetStringAsync(
                    cacheKey, 
                    value, 
                    new DistributedCacheEntryOptions 
                    { 
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) 
                    }, 
                    cancellationToken);
            }
        }
    }
}
