using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class IconRepository(ApplicationDbContext db) : Repository<Icon>(db), IIconRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Icon obj)
        {
            _db.Icons.Update(obj);
        }
    }
}
