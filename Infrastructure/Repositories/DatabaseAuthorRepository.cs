using Microsoft.EntityFrameworkCore;
using task_1135.Domain.Models;
using task_1135.Domain.Repositories;

namespace task_1135.Infrastructure.Repositories
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
            return await _context.Authors.ToListAsync();
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
