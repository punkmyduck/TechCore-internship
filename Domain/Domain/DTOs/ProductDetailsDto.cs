using Domain.Models;

namespace Domain.DTOs
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
