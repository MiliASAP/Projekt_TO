using Projekt_TO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Projekt_TO.Data.Services;
using System.Threading;
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
        Console.Clear();
        Thread.Sleep(1000);
        Console.Clear();
    }

    public void DisplayRentals(IEnumerable<Rental> rentals)
    {
        if (rentals == null || !rentals.Any())
        {
            Console.Clear();
            Console.WriteLine("Nie masz wynajętych żadnych samochodów.");
            Thread.Sleep(1000);
            Console.Clear();
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
        Console.Clear();
        Console.WriteLine("Pojazd dodany.");
        Thread.Sleep(1000); 
        Console.Clear();
    }

    public void DisplayCarRemoved()
    {
        Console.Clear();
        Console.WriteLine("Pojazd usunięty.");
        Thread.Sleep(1000); 
        Console.Clear();
    }

    public void DisplayCarUpdated()
    {
        Console.Clear();
        Console.WriteLine("Pojazd zaktualizowany.");
        Thread.Sleep(1000);
        Console.Clear();
    }
}