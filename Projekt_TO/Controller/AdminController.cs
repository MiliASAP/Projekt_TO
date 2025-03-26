using Projekt_TO.Data.Services;
using System;

namespace Projekt_TO.Controller
{
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
}
