using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;

namespace FoodTracker.DataAccess.Repository
{
    public class MealItemRepository(ApplicationDbContext db) : Repository<MealItem>(db), IMealItemRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(MealItem obj)
        {
            _db.MealItems.Update(obj);
        }
    }
}
