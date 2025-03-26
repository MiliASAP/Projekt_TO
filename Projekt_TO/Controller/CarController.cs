using Projekt_TO.Data.Services;
using Projekt_TO.Model;
using System;
using System.Linq;

namespace Projekt_TO.Controller
{
    public class CarController : ICarController
    {
        public void UpdateRentalStatus(IRentalDbContext db)
        {
            var cars = db.Cars.ToList();

            foreach (var car in cars)
            {
                var expiredRentals = db.Rentals.Where(rent => rent.CarId == car.Id && rent.EndDate < DateTime.Now).ToList();
                if (expiredRentals.Any())
                {
                    car.IsRent = false;
                    db.Rentals.RemoveRange(expiredRentals);
                }
            }

            db.SaveChanges();
        }
        public void ShowAvailableCars(IRentalDbContext db, IUserView view)
        {
            UpdateRentalStatus(db);

            var cars = db.Cars.ToList();
            view.DisplayAvailableCars(cars);
        }

        public void AddCar(IRentalDbContext db, IUserView view)
        {
            Console.Write("Marka: ");
            string brand = Console.ReadLine();
            Console.Write("Model: ");
            string model = Console.ReadLine();
            Console.Write("Konie mechaniczne: ");
            int hp = int.Parse(Console.ReadLine());
            Console.Write("Cena za dobę: ");
            decimal price = decimal.Parse(Console.ReadLine());

            db.Cars.Add(new Car { Brand = brand, Model = model, HorsePower = hp, RentalPricePerDay = price });
            db.SaveChanges();
            view.DisplayCarAdded();
        }

        public void RemoveCar(IRentalDbContext db, IUserView view)
        {
            Console.Write("ID pojazdu do usunięcia: ");
            int id = int.Parse(Console.ReadLine());
            var car = db.Cars.Find(id);
            if (car != null)
            {
                db.Cars.Remove(car);
                db.SaveChanges();
                view.DisplayCarRemoved();
            }
        }

        public void EditCar(IRentalDbContext db, IUserView view)
        {
            Console.Write("ID pojazdu do edycji: ");
            int id = int.Parse(Console.ReadLine());
            var car = db.Cars.Find(id);
            if (car != null)
            {
                Console.Write("Nowa cena za dobę: ");
                car.RentalPricePerDay = decimal.Parse(Console.ReadLine());
                db.SaveChanges();
                view.DisplayCarUpdated();
            }
        }

    }
}
