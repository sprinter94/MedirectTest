using Medirect.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medirect.Infrastructure.Persistance
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user", schema: "testr");
            });

            modelBuilder.Entity<ExchangeRatesHistory>(entity =>
            {
                entity.HasOne(a => a.User)
                .WithMany(b => b.ExchangeRatesHistory)
                .HasForeignKey(a => a.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("exchangerateshistory", schema: "testr");
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ExchangeRatesHistory> ExchangeRatesHistories { get; set; }
    }
}