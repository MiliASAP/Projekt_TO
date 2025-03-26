namespace Projekt_TO.Data.Services
{
    public interface ICarController
    {
        void ShowAvailableCars(IRentalDbContext db, IUserView view);
        void AddCar(IRentalDbContext db, IUserView view);
        void RemoveCar(IRentalDbContext db, IUserView view);
        void EditCar(IRentalDbContext db, IUserView view);
        void UpdateRentalStatus(IRentalDbContext db);

    }
}
