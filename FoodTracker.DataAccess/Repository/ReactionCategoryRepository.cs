using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Event;
using FoodTracker.Models.Reaction;

namespace FoodTracker.DataAccess.Repository
{
    public class ReactionCategoryRepository(ApplicationDbContext db) : Repository<ReactionCategory>(db), IReactionCategoryRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ReactionCategory obj)
        {
            _db.ReactionCategories.Update(obj);
        }
    }
}
