using Microsoft.EntityFrameworkCore;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;

namespace task_1135.Infrastructure.Repositories
{
    public class DatabaseBookRepository : IBookRepository
    {
        private readonly BookContext _context;
        public DatabaseBookRepository(BookContext context)
        {
            _context = context;
        }
        public async Task Add(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetById(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task Update(int id, Book updatedBook)
        {
            var book = await GetById(id);
            if (book == null) throw new InvalidOperationException("Book with this id not found");
            book.Title = updatedBook.Title;
            book.AuthorId = updatedBook.AuthorId;
            book.YearPublished = updatedBook.YearPublished;
            await _context.SaveChangesAsync();
        }
    }
}
