using Domain.Models;

namespace task1135.Infrastructure.Storage
{
    public class BookStorage
    {
        public List<Book> Books;
        public BookStorage()
        {
            Books = new();
        }
    }
}
