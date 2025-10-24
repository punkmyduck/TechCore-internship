using Microsoft.EntityFrameworkCore;
using task_1135.Domain.Models;

namespace task_1135.Infrastructure
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books => Set<Book>();
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
