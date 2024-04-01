using FoodTracker.Models.Activity;
using FoodTracker.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IActivityIntensityRepository : IRepository<ActivityIntensity>
    {
     public void Update(ActivityIntensity obj);
    }
}
