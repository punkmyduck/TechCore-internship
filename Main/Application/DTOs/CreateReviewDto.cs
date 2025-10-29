namespace task_1135.Application.DTOs
{
    public class CreateReviewDto
    {
        public string ProductId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
