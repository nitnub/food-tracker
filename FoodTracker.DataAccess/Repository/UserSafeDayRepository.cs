using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Day;

namespace FoodTracker.DataAccess.Repository
{
    public class UserSafeDayRepository(ApplicationDbContext db) : Repository<UserSafeDay>(db), IUserSafeDayRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void Update(UserSafeDay obj)
        {
            _db.UserSafeDays.Update(obj);
        }
    }
}
