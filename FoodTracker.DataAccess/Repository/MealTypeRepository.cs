using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;
using FoodTracker.Models;
using FoodTracker.Models.Meal;

namespace FoodTracker.DataAccess.Repository
{
    public class MealTypeRepository(ApplicationDbContext db) : Repository<MealType>(db), IMealTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(MealType obj)
        {
            _db.MealTypes.Update(obj);
        }
    }
}
