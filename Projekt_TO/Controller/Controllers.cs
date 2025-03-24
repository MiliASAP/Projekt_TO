using System.Linq;
using System;
using System.Threading;

public interface IUserController
{
    User Login(IRentalDbContext db, IUserView view);
    void ShowUserRentals(IRentalDbContext db, User user, IUserView view);
    void RentCar(IRentalDbContext db, User user, IUserView view);

    void UserPanel(IRentalDbContext db, User user, IUserController userController, ICarController carController,
        IUserView userView);
}

public interface ICarController
{
    void ShowAvailableCars(IRentalDbContext db, IUserView view);
    void AddCar(IRentalDbContext db, IUserView view);
    void RemoveCar(IRentalDbContext db, IUserView view);
    void EditCar(IRentalDbContext db, IUserView view);
    void UpdateRentalStatus(IRentalDbContext db);

}

public interface IAdminController
{
    void AdminPanel(IRentalDbContext db, IUserView view);
}


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
            Thread.Sleep(1000);
            Console.Clear();
            return Login(db, view);
        }
        return user;
    }



    public void ShowUserRentals(IRentalDbContext db, User user, IUserView view)
    {
        var rentals = db.Rentals.Where(r => r.UserId == user.Id).ToList();
        _carController.UpdateRentalStatus(db);
        view.DisplayRentals(rentals);
    }

    public void RentCar(IRentalDbContext db, User user, IUserView view)
    {
        Console.Write("ID auta: ");
        int carId = int.Parse(Console.ReadLine());
        Console.Write("Ilość dni: ");
        int days = int.Parse(Console.ReadLine());

        _carController.UpdateRentalStatus(db);

        var car =  db.Cars.FirstOrDefault(c => c.Id == carId);
        if (car == null)
        {
            Console.WriteLine("Pojazd o podanym ID nie istnieje w bazie.");
            return;
        }
        var currentRental = db.Rentals
            .FirstOrDefault(r => r.CarId == carId && r.EndDate >= DateTime.Now);
        if (currentRental != null)
        {
            Console.WriteLine("Pojazd jest już wynajęty.");
        }

        if (!car.IsRent)
        {
                db.Rentals.Add(new Rental { UserId = user.Id, CarId = carId, StartDate = DateTime.Now, Days = days });
                car.IsRent = true;
                db.SaveChanges();
                Console.WriteLine("Auto wynajęte!");
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
                    _carController.ShowAvailableCars(db, userView);
                    break;
                case "2":
                    userController.RentCar(db, user, userView);
                    break;
                case "3":
                    ShowUserRentals(db, user, userView);
                    break;
                case "4":
                    return;
            }
        }
    }
}

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

public class AdminController : IAdminController
{
    private readonly ICarController _carController;

    public AdminController(ICarController carController)
    {
        _carController = carController;
    }

    public void AdminPanel(IRentalDbContext db, IUserView view)
    {
        while (true)
        {
            Console.WriteLine("1. Wyświetl dostępne auta\n2. Dodaj pojazd\n3. Usuń pojazd\n4. Edytuj pojazd\n5. Wyjście");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    _carController.ShowAvailableCars(db, view);
                    break;
                case "2":
                    _carController.AddCar(db, view);
                    break;
                case "3":
                    _carController.RemoveCar(db, view);
                    break;
                case "4":
                    _carController.EditCar(db, view);
                    break;
                case "5":
                    return;
            }
        }
    }
    
}


