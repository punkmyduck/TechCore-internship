namespace task_1135.Application.DTOs
{
    public class UpdateReviewDto
    {
        public string? Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = null!;
    }
}
