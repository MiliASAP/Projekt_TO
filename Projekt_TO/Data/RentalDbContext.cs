using Microsoft.EntityFrameworkCore;
using Projekt_TO.Data.Services;
using Projekt_TO.Model;

namespace Projekt_TO.Data
{
    public class RentalDbContext : DbContext, IRentalDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-6A45O9RJ\\SQLEXPRESS;Database=proj_TO;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
}
