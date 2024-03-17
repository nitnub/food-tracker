using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class ActivityIntensityRepository(ApplicationDbContext db) : Repository<ActivityIntensity>(db), IActivityIntensityRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ActivityIntensity obj)
        {
            _db.ActivityIntensities.Update(obj);
        }
    }
}
