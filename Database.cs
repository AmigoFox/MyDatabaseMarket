using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;


namespace MyDatabaseMarket
{
    public class Database
    {
        public class User
        {
            [Key]
            public int Id { get; set; }
            public string Login { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string PasswordHash { get; set; } = null!;
            public string RegistrationDate { get; set; } = null!;

            public ICollection<Order> Orders { get; set; } = new List<Order>();
            public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        }

        public class Order
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string DatabaseType { get; set; } = null!;
            public int SizeGB { get; set; }
            public string IOPS { get; set; }
            public string StorageType { get; set; } = null!;
            public string Country { get; set; } = null!;
            public decimal PriceUSD { get; set; }
            public decimal PriceRUB { get; set; }
            public string OrderDate { get; set; } = null!;
            public string Status { get; set; } = null!;
            public string Scalability { get; set; } = "None";

            public ICollection<Payment> Payments { get; set; } = new List<Payment>();
            public User User { get; set; } = null!;
        }


        public class Payment
        {
            public int Id { get; set; }
            public decimal AmountRUB { get; set; }
            public decimal AmountUSD { get; set; }

            public int OrderId { get; set; }
            public Order Order { get; set; }

            public int UserId { get; set; }
            public User User { get; set; }

            public string PaymentDate { get; set; }
            public string Status { get; set; }

            public string? NextPaymentDate { get; set; }
        }


        public class CalculatorData
        {
            [Key]
            public int Id { get; set; }
            public int UserId { get; set; }

            public string DatabaseType { get; set; } = null!;
            public int SizeGB { get; set; }
            public string StorageType { get; set; } = null!;
            public int IOPS { get; set; } = 100;
            public string Scalability { get; set; } = null!;
            public string Countries { get; set; } = null!; // Сохраняем через запятую

            public decimal PriceRUB { get; set; }
            public decimal PriceUSD { get; set; }

            public User User { get; set; } = null!;
        }

        // Контекст базы данных
        public class AppDbContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Payment> Payments { get; set; }

            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }



            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasIndex(e => e.Login).IsUnique();
                    entity.HasIndex(e => e.Email).IsUnique();
                    entity.Property(e => e.RegistrationDate)
                          .HasColumnType("TEXT")
                          .HasDefaultValueSql("CURRENT_TIMESTAMP");
                });

                modelBuilder.Entity<Order>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.OrderDate)
                          .HasColumnType("TEXT")
                          .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    entity.Property(e => e.Status).HasDefaultValue("Active");

                    entity.HasOne(o => o.User)
                          .WithMany(u => u.Orders)
                          .HasForeignKey(o => o.UserId)
                          .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<Payment>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.PaymentDate)
                          .HasColumnType("TEXT")
                          .HasDefaultValueSql("CURRENT_TIMESTAMP");
                    entity.Property(e => e.Status).HasDefaultValue("Paid");

                    entity.HasOne(p => p.User)
                          .WithMany(u => u.Payments)
                          .HasForeignKey(p => p.UserId)
                          .OnDelete(DeleteBehavior.Cascade);

                    entity.HasOne(p => p.Order)
                          .WithMany(o => o.Payments)
                          .HasForeignKey(p => p.OrderId)
                          .OnDelete(DeleteBehavior.Cascade);
                });

                modelBuilder.Entity<CalculatorData>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.HasOne(e => e.User)
                          .WithMany()
                          .HasForeignKey(e => e.UserId)
                          .OnDelete(DeleteBehavior.Cascade);
                });
            }



        }
    }
}