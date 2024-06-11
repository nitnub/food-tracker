using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;

namespace FoodTracker.DataAccess.Repository
{
    public class FoodAliasRepository(ApplicationDbContext db) : Repository<FoodAlias>(db), IFoodAliasRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(FoodAlias obj)
        {
            _db.FoodAliases.Update(obj);
        }
    }
}
