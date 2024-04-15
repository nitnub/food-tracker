using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class ReactionSourceTypeRepository(ApplicationDbContext db) : Repository<ReactionSourceType>(db), IReactionSourceTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ReactionSourceType obj)
        {
            _db.ReactionSourceTypes.Update(obj);
        }
    }
}
