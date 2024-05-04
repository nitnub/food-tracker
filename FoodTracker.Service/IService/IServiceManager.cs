using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Service.IService
{
    public interface IServiceManager
    {
        public IActivityService Activity { get; }
        public ICalendarService Calendar { get; }
        public IFoodService Food { get; }
        public IFodmapService Fodmap { get; }
        public IReactionService Reaction { get; }
        public IMealService Meal { get; }
        public IUtilityService Utility { get; }
    }
}
