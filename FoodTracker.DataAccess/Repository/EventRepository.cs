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
    public class EventRepository(ApplicationDbContext db) : Repository<Event>(db), IEventRepository
    {
        private ApplicationDbContext _db = db;

        public void Update(Event obj)
        {
            _db.Events.Update(obj);
        }
    }
}
