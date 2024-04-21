using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;

namespace FoodTracker.DataAccess.Repository
{
    public class UnitTypeRepository(ApplicationDbContext db) : Repository<UnitType>(db), IUnitTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(UnitType obj)
        {
            _db.UnitTypes.Update(obj);
        }
    }
}
