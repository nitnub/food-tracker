using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class UserSafeFoodRepository(ApplicationDbContext db) : Repository<UserSafeFood>(db), IUserSafeFoodRepository
    {
        private readonly ApplicationDbContext _db = db;

        public void Update(UserSafeFood obj)
        {
            _db.UserSafeFoods.Update(obj);
        }
    }
}
