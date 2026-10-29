
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LAB5
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<CategorieProdus> CategoriiProduse { get; set; }
        public DbSet<IstoricVanzari> IstoricVanzari { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "MagazinDB.db");

                optionsBuilder.UseSqlite(
                    $"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IstoricVanzari>()
                .HasOne(s => s.Produs)
                .WithMany()
                .HasForeignKey(s => s.IdProdus)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
