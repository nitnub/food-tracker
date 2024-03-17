using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.FODMAP;

namespace FoodTracker.DataAccess.Repository
{
    public class FodmapAliasRepository(ApplicationDbContext db) : Repository<FodmapAlias>(db), IFodmapAliasRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(FodmapAlias obj)
        {
            _db.FodmapAliases.Update(obj);
        }
    }
}
