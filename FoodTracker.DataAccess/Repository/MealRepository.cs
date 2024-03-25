using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;

namespace FoodTracker.DataAccess.Repository
{
    public class MealRepository(ApplicationDbContext db) : Repository<Meal>(db), IMealRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Meal obj)
        {
            _db.Meals.Update(obj);
        }
    }
}
