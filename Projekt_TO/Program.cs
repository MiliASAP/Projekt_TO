using Microsoft.EntityFrameworkCore;
using Projekt_TO.Data;
using Projekt_TO.Data.Services;
using Projekt_TO.Controller;
using Projekt_TO.Model;
using System;

class Program
{
    static void Main()
    {
        var dbContext = new RentalDbContext();
        dbContext.Database.Migrate();

        IUserView userView = new ConsoleUserView();
        ICarController carController = new CarController();
        IUserController userController = new UserController(carController);
        IAdminController adminController = new AdminController(carController);

        Console.WriteLine("Witaj w systemie wypożyczalni samochodów!");
        User currentUser = userController.Login(dbContext, userView);

        if (currentUser.IsAdmin)
        {
            adminController.AdminPanel(dbContext, userView);
        }
        else
        {
            userController.UserPanel(dbContext, currentUser, userController, carController, userView);
        }


    }

    

}