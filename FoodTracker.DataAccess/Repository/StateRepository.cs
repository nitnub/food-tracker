using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Food;

namespace FoodTracker.DataAccess.Repository
{
    public class StateRepository(ApplicationDbContext db) : Repository<State>(db), IStateRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(State obj)
        {
            _db.States.Update(obj);
        }
    }
}
