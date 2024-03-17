using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;

namespace FoodTracker.DataAccess.Repository
{
    public class IngredientMapRepository(ApplicationDbContext db) : Repository<IngredientMap>(db), IIngredientMapRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(IngredientMap obj)
        {
            _db.IngredientMaps.Update(obj);
        }
    }
}
