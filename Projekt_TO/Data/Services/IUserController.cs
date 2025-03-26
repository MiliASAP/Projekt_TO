using Projekt_TO.Model;

namespace Projekt_TO.Data.Services
{
    public interface IUserController
    {
        User Login(IRentalDbContext db, IUserView view);
        void ShowUserRentals(IRentalDbContext db, User user, IUserView view);
        void RentCar(IRentalDbContext db, User user, IUserView view);

        void UserPanel(IRentalDbContext db, User user, IUserController userController, ICarController carController,
            IUserView userView);
    }
}
