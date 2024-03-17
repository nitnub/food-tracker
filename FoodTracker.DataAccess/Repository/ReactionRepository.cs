using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class ReactionRepository(ApplicationDbContext db) : Repository<Reaction>(db), IReactionRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Reaction obj)
        {
            _db.Reactions.Update(obj);
        }
    }
}
