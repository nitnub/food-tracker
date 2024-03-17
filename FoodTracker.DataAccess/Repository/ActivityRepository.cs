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
    public class ActivityRepository(ApplicationDbContext db) : Repository<Activity>(db), IActivityRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Activity obj)
        {
            _db.Activities.Update(obj);
        }
    }
}
