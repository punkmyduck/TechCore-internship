namespace task_1135.Domain.Models
{
    public class Book
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Author { get; private set; }

        private int _yearPublished;
        public int YearPublished 
        { 
            get => _yearPublished; 
            private set
            {
                if (value < 0 || value > DateTime.Now.Year)
                {
                    throw new ArgumentOutOfRangeException(nameof(YearPublished), "YearPublished must be a valid year.");
                }
                _yearPublished = value;
            }
        }

        public Book(string title, string author, int yearPublished)
        {
            Id = _id++;
            Title = title;
            Author = author;
            YearPublished = yearPublished;
        }

        public void UpdateDetails(string title, string author, int yearPublished)
        {
            Title = title;
            Author = author;
            YearPublished = yearPublished;
        }
    }
}