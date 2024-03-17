using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository
{
    public class EventTypeRepository(ApplicationDbContext db) : Repository<EventType>(db), IEventTypeRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(EventType obj)
        {
            _db.EventTypes.Update(obj);
        }
    }
}
