using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;

namespace FoodTracker.DataAccess.Repository
{
    public class ColorRepository(ApplicationDbContext db) : Repository<Color>(db), IColorRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Color obj)
        {
            _db.Colors.Update(obj);
        }
    }
}
