using Microsoft.EntityFrameworkCore;
using System.IO;

namespace LAB5
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

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
    }
}