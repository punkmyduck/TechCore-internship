using task_1135.Domain.Models;

namespace task_1135.Application.DTOs
{
    public class ProductDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int YearPublished { get; set; }
        public double AverageProductRating { get; set; }
        public List<ReturnAuthorShortDto> Authors { get; set; } = new();
        public List<ProductReview> Reviews { get; set; } = new();
    }
}
