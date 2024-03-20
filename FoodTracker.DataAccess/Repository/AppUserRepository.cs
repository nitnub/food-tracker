using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository
{


    public class AppUserRepository(ApplicationDbContext db) : Repository<AppUser>(db), IAppUserRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(AppUser AppUser)
        {
            _db.AppUsers.Update(AppUser);
        }


    }
}
