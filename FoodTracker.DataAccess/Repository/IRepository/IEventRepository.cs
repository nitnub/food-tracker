using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IEventRepository : IRepository<Event>
    {
     public void Update(Event obj);
    }
}
