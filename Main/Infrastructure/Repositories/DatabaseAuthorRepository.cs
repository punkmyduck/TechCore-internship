using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Domain.Repositories;

namespace task1135.Infrastructure.Repositories
{
    public class DatabaseAuthorRepository : IAuthorRepository
    {
        private readonly BookContext _context;
        public DatabaseAuthorRepository(BookContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _context.Authors.Where(a => a.Id == id).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.AsNoTracking().ToListAsync();
        }

        public Task<Author?> GetByIdAsync(int id)
        {
            return _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
