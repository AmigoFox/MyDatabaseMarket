using Microsoft.EntityFrameworkCore;
using MyDatabaseMarket.Models;
using static MyDatabaseMarket.Database;

namespace MyDatabaseMarket.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка уникального индекса для логина
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Login)
                .IsUnique();
        }
    }
}