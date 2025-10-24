namespace task_1135.Domain.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearPublished { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}