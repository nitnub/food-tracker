using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository
{
    public class ActivityTypeRepository(ApplicationDbContext db) : Repository<ActivityType>(db), IActivityTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(ActivityType obj)
        {
            _db.ActivityTypes.Update(obj);
        }
    }
}
