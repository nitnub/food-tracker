using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.FODMAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository
{
    public class FodmapCategoryRepository(ApplicationDbContext db) : Repository<FodmapCategory>(db), IFodmapCategoryRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(FodmapCategory obj)
        {
            _db.FodmapCategories.Update(obj);
        }
    }
}
