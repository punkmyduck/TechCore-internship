namespace task_1135.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int YearPublished { get; set; }
        public List<Author> Authors { get; set; } = null!;
    }
}