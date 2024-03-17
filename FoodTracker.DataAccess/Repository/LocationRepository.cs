using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class LocationRepository(ApplicationDbContext db) : Repository<Location>(db), ILocationRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Location obj)
        {
            _db.Locations.Update(obj);
        }
    }
}
