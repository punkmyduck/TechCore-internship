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
        public async Task Add(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            await _context.Authors.Where(a => a.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Author>> GetAll()
        {
            return await _context.Authors.ToListAsync();
        }

        public Task<Author?> GetById(int id)
        {
            return _context.Authors.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
