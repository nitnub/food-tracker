using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    public class CalendarService(string userId, IUnitOfWork unitOfWork) : ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IReactionService _reactionService = new ReactionService(userId, unitOfWork);

        public string GetDayColor(DateTime day)
        {
            var dayColor = SD.COLOR_BLUE;
            var dayReactions = _reactionService.GetAllDayReactions(day);

            if (_reactionService.IsUserSafeDay(day))
            {
                dayColor = SD.COLOR_GREEN;
            }
            else if (dayReactions.Count > 0)
            {
                dayColor = Helper.GetColorStringFromSeverity(dayReactions.Select(r => 
                                                                    r.Severity.Value).Max()).ToLower();
            }
 
            return dayColor.ToLower();
        }
        public DayVM[,] GetPopulatedCalendarDays(
                                    //Dictionary<string, List<Meal>> mealDict,
                                    //Dictionary<string, List<Reaction>> reactionDict,
                                    //Dictionary<string, Icon> iconDict,
                                    //Dictionary<string, bool> userSafeDays,
                                    DateTime dt
            )
        {
            throw new NotImplementedException();
        }

        public DayVM[,] GetPopulatedCalendarDays(Dictionary<string, List<Meal>> mealDict, Dictionary<string, List<Reaction>> reactionDict, Dictionary<string, Icon> iconDict, Dictionary<string, bool> userSafeDays, DateTime dt)
        {
            throw new NotImplementedException();
        }
    }
}
