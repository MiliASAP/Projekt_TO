using Projekt_TO.Model;
using System.Collections.Generic;


namespace Projekt_TO.Data.Services
{
    public interface IUserView
    {
        string GetUsername();
        string GetPassword();
        void DisplayLoginFailed();
        void DisplayRentals(IEnumerable<Rental> rentals);
        void DisplayAvailableCars(IEnumerable<Car> cars);
        void DisplayCarAdded();
        void DisplayCarRemoved();
        void DisplayCarUpdated();
    }
}
