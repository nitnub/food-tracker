using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
using System;

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

        public Dictionary<int, string> GetDayColorByMonth(DateTime date, Dictionary<int, List<Reaction>> reactions)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            var output = new Dictionary<int, string>();

            var dayColor = SD.COLOR_BLUE;
            var userSafeDays = _reactionService.GetUserSafeDaysDict(date);
            //var dayReactions = _reactionService.GetAllDayReactionsForTheMonth(date);
            for (int i = 1; i <= daysInMonth; i++)
            {

                if (userSafeDays.ContainsKey($"{date.Year}-{date.Month:D2}-{i:D2}"))
                {
                    dayColor = SD.COLOR_GREEN;
                    //output[i] = SD.COLOR_GREEN.ToLower();
                }
                else if (reactions[i].Count > 0)
                {
                    dayColor = Helper.GetColorStringFromSeverity(reactions[i].Select(r =>
                                                                        r.Severity.Value).Max()).ToLower();
                    //output[i] = dayColor.ToLower();
                }
                else
                {
                    dayColor = SD.COLOR_BLUE;
                }
                output[i] = dayColor.ToLower();
            }
            return output;
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
