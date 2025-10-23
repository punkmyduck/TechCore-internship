using task_1135.Domain.Models;

namespace task_1135.Infrastructure.Storage
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
