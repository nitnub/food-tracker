using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class ReactionTypeRepository(ApplicationDbContext db) : Repository<ReactionType>(db), IReactionTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ReactionType obj)
        {
            _db.ReactionTypes.Update(obj);
        }
    }
}
