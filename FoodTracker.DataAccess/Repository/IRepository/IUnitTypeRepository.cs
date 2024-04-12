using FoodTracker.Models;
using FoodTracker.Models.Activity;
using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IUnitTypeRepository : IRepository<UnitType>
    {
     public void Update(UnitType obj);
    }
}
