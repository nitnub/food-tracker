using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class UnitRepository(ApplicationDbContext db) : Repository<Unit>(db), IUnitRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Unit obj)
        {
            _db.Units.Update(obj);
        }
    }
}
