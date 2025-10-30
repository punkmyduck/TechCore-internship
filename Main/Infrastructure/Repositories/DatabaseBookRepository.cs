using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Repositories;

namespace task1135.Infrastructure.Repositories
{
    public class DatabaseBookRepository : IBookRepository
    {
        private readonly BookContext _context;
        public DatabaseBookRepository(BookContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task AddBookAuthorAsync(int bookId, int authorId)
        {
            var book = await GetByIdAsync(bookId);
            var author = await _context.Authors.FindAsync(authorId);

            if (book == null || author == null) throw new Exception("Book or Author not found");

            if (!book.Authors.Any(a => a.Id == authorId)) book.Authors.Add(author);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _context.Books
                .Where(b => b.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            var book = await _context.Books.Include(b => b.Authors).FirstOrDefaultAsync(b => b.Id == id);
            return book;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Book updatedBook)
        {
            var book = await GetByIdAsync(id);
            if (book == null) throw new KeyNotFoundException($"Book with Id = {id} not found");
            book.Title = updatedBook.Title;
            book.YearPublished = updatedBook.YearPublished;
        }
    }
}
