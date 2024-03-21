using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using FoodTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IStateRepository : IRepository<State>
    {
     public void Update(State obj);
    }
}
