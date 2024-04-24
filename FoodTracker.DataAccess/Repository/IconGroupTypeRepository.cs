using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class IconGroupTypeRepository(ApplicationDbContext db) : Repository<IconGroupType>(db), IIconGroupTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(IconGroupType obj)
        {
            _db.IconGroupTypes.Update(obj);
        }
    }
}
