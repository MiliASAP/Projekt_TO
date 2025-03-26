using Microsoft.EntityFrameworkCore;
using Projekt_TO.Model;

namespace Projekt_TO.Data.Services
{
    public interface IRentalDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Car> Cars { get; set; }
        DbSet<Rental> Rentals { get; set; }
        int SaveChanges();
    }
}
