using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;
using FoodTracker.Models.Food;

namespace FoodTracker.DataAccess.Repository
{
    public class FoodRepository(ApplicationDbContext db) : Repository<Food>(db), IFoodRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Food obj)
        {
            _db.Food.Update(obj);
        }
    }
}
