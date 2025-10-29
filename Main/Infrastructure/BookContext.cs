using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using task_1135.Domain.Models;

namespace task_1135.Infrastructure
{
    public class BookContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public DbSet<Book> Books => Set<Book>();
        public DbSet<Author> Authors => Set<Author>();
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Authors)
                .WithMany(a => a.Books)
                .UsingEntity(j => j.ToTable("BookAuthors"));

            base.OnModelCreating(modelBuilder);
        }
    }
}
