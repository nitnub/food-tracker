using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class ReactionSeverityRepository(ApplicationDbContext db) : Repository<ReactionSeverity>(db), IReactionSeverityRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ReactionSeverity obj)
        {
            _db.ReactionSeverities.Update(obj);
        }
    }
}
