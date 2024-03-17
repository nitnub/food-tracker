using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;

namespace FoodTracker.DataAccess.Repository
{
    public class FodmapRepository(ApplicationDbContext db) : Repository<Fodmap>(db), IFodmapRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Fodmap obj)
        {
            _db.Fodmaps.Update(obj);
        }
    }
}
