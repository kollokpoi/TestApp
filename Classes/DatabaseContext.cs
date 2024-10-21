using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Classes
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<CounterReading> CounterReadings { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<Adress> Adresses { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apartment>()
                .HasOne(a => a.Counter)
                .WithOne(c => c.Apartment)
                .HasForeignKey<Counter>(c => c.ApartmentId);  // Указываем внешний ключ для зависимой стороны
        }
    }
}
