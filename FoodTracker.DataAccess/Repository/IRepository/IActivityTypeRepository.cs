using FoodTracker.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IActivityTypeRepository : IRepository<ActivityType>
    {
     public void Update(ActivityType obj);
    }
}
