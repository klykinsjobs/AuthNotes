using AuthNotes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthNotes.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Note> Notes => Set<Note>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Note>()
                .HasOne(n => n.LastUpdatedBy)
                .WithMany(u => u.Notes)
                .HasForeignKey(n => n.LastUpdatedByUserId);
        }
    }
}
