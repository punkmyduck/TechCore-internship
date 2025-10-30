namespace Domain.DTOs
{
    public class ReturnBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int YearPublished { get; set; }
        public List<ReturnAuthorShortDto> Authors { get; set; } = new();
    }
}
