namespace Projekt_TO.Data.Services
{
    public interface IAdminController
    {
        void AdminPanel(IRentalDbContext db, IUserView view);
    }
}
