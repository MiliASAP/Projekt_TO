using System.Linq;
using System;
using System.Threading;
using Projekt_TO.Data.Services;
using Projekt_TO.Model;

public class UserController : IUserController
{
    private readonly ICarController _carController;

    public UserController(ICarController carController)
    {
        _carController = carController;
    }

    public User Login(IRentalDbContext db, IUserView view)
    {
        string username = view.GetUsername();
        string password = view.GetPassword();

        User user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null)
        {
            view.DisplayLoginFailed();
            return Login(db, view);
        }
        Console.Clear();
        return user;
    }



    public void ShowUserRentals(IRentalDbContext db, User user, IUserView view)
    {
        var rentals = db.Rentals.Where(r => r.UserId == user.Id).ToList();
        _carController.UpdateRentalStatus(db);
        view.DisplayRentals(rentals);
    }

    public void RentCar(IRentalDbContext db, User user, IUserView view, ICarController carController)
    {
        _carController.UpdateRentalStatus(db);
        _carController.ShowAvailableCars(db,view);
        if(db.Cars.Count() == 0)
        {
            return;
        }

        Console.Write("ID auta: ");
        int carId = int.Parse(Console.ReadLine());

        var car = db.Cars.FirstOrDefault(c => c.Id == carId);

        if (car == null)
        {
            Console.Clear();
            Console.WriteLine("Pojazd o podanym ID nie istnieje w bazie.");
            Thread.Sleep(1000);
            Console.Clear();
            return;
        }

        Console.Write("Ilość dni: ");
        int days = int.Parse(Console.ReadLine()); 

        var currentRental = db.Rentals
            .FirstOrDefault(r => r.CarId == carId && r.EndDate >= DateTime.Now);
        if (currentRental != null)
        {
            Console.Clear();
            Console.WriteLine("Pojazd jest już wynajęty.");
            Thread.Sleep(1000);
            Console.Clear();
        }

        if (!car.IsRent)
        {
                db.Rentals.Add(new Rental { UserId = user.Id, CarId = carId, StartDate = DateTime.Now, Days = days });
                car.IsRent = true;
                db.SaveChanges();
            Console.Clear();
                Console.WriteLine("Auto wynajęte!");
            Thread.Sleep(1000);
            Console.Clear();
        }
       
    }
    public void UserPanel(IRentalDbContext db, User user, IUserController userController, ICarController carController, IUserView userView)
    {
        while (true)
        {
            Console.WriteLine("1. Wyświetl dostępne auta\n2. Wynajmij auto\n3. Moje wynajmy\n4. Wyjście");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Clear();
                    _carController.ShowAvailableCars(db, userView);
                    break;
                case "2":
                    Console.Clear();
                    userController.RentCar(db, user, userView, carController);
                    break;
                case "3":
                    Console.Clear();
                    ShowUserRentals(db, user, userView);
                    break;
                case "4":
                    return;
            }
        }
    }
}



