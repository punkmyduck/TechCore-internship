namespace task_1135.Application.DTOs
{
    public class GetBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int YearPublished { get; set; }
        public List<GetAuthorShortDto> Authors { get; set; } = new();
    }
}
