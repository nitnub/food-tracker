using FoodTracker.Models.Activity;
using FoodTracker.Models.Day;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IUserSafeDayRepository : IRepository<UserSafeDay>
    {
        public void Update(UserSafeDay obj);
    }
}
