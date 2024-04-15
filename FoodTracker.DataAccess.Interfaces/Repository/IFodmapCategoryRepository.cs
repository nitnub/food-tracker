using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IFodmapCategoryRepository : IRepository<FodmapCategory>
    {
     public void Update(FodmapCategory obj);
    }
}
