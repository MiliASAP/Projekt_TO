using Projekt_TO.Model;
using System;
using System.Collections.Generic;
using System.Linq;

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

public class ConsoleUserView : IUserView
{
    public string GetUsername()
    {
        Console.Write("Podaj login: ");
        return Console.ReadLine();
    }

    public string GetPassword()
    {
        Console.Write("Podaj hasło: ");
        return Console.ReadLine();
    }

    public void DisplayLoginFailed()
    {
        Console.WriteLine("Niepoprawne dane logowania!");
    }

    public void DisplayRentals(IEnumerable<Rental> rentals)
    {
        if (rentals == null || !rentals.Any())
        {
            Console.WriteLine("Nie masz wynajętych żadnych samochodów.");
            return;
        }

        foreach (var rent in rentals)
        {
            int remainingDays = Math.Max((rent.EndDate - DateTime.Now).Days, 0);
            Console.WriteLine($"Auto ID: {rent.CarId}, Wynajem do: {rent.EndDate}, Pozostało dni: {remainingDays}");
        }
    }


    public void DisplayAvailableCars(IEnumerable<Car> cars)
    {
        foreach (var car in cars)
        {
            Console.WriteLine($"ID: {car.Id}, {car.Brand} {car.Model}, {car.HorsePower} KM, {car.RentalPricePerDay} zł/dzień, {(car.IsRent ? "wynajęty" : "wolny")}");
        }
    }

    public void DisplayCarAdded()
    {
        Console.WriteLine("Pojazd dodany.");
    }

    public void DisplayCarRemoved()
    {
        Console.WriteLine("Pojazd usunięty.");
    }

    public void DisplayCarUpdated()
    {
        Console.WriteLine("Pojazd zaktualizowany.");
    }
}