using LibraryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<BookIssue> BookIssues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookIssue>()
                .HasOne(bi => bi.Book)
                .WithMany(b => b.BookIssues)
                .HasForeignKey(bi => bi.BookId);

            modelBuilder.Entity<BookIssue>()
                .HasOne(bi => bi.Reader)
                .WithMany(r => r.BookIssues)
                .HasForeignKey(bi => bi.ReaderId);

            modelBuilder.Entity<BookIssue>(entity =>
            {
                entity.Property(e => e.IssueDate).HasColumnType("timestamp");
            });

            modelBuilder.Entity<BookIssue>(entity =>
            {
                entity.Property(e => e.ReturnDate).HasColumnType("timestamp");
            });
        }
    }
}
