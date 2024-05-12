using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Reaction;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    public class CalendarService(string userId, IUnitOfWork unitOfWork) : ICalendarService
    {
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

        public Dictionary<int, string> GetDayColorsForSurroundingMonths(DateTime date, Dictionary<int, List<Reaction>> reactions)
        {
            var dh = new DateHelper(date);
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);
            var output = new Dictionary<int, string>();
            var dayColor = SD.COLOR_BLUE;
            
            var userSafeDays = _reactionService.GetUserSafeDaysForSurroundingMonths(date);

            for (int i = -7; i <= daysInMonth + 7; i++)
            {
                if (userSafeDays.ContainsKey(dh.GetTodayFromDayIndex(i).ToString(SD.DATE_FORMAT)))
                {
                    dayColor = SD.COLOR_GREEN;
                }
                else if (reactions[i].Count > 0)
                {
                    dayColor = Helper.GetColorStringFromSeverity(reactions[i].Select(r =>
                                                                        r.Severity.Value).Max()).ToLower();
                }
                else
                {
                    dayColor = SD.COLOR_BLUE;
                }
                output[i] = dayColor.ToLower();
            }
            return output;
        }

        public Dictionary<int, string> GetDayColorByMonth(DateTime date, Dictionary<int, List<Reaction>> reactions)
        {
            var daysInMonth = DateTime.DaysInMonth(date.Year, date.Month);

            var output = new Dictionary<int, string>();

            var dayColor = SD.COLOR_BLUE;
            var userSafeDays = _reactionService.GetUserSafeDaysDict(date);

            for (int i = 1; i <= daysInMonth; i++)
            {

                if (userSafeDays.ContainsKey($"{date.Year}-{date.Month:D2}-{i:D2}"))
                {
                    dayColor = SD.COLOR_GREEN;
                }
                else if (reactions[i].Count > 0)
                {
                    dayColor = Helper.GetColorStringFromSeverity(reactions[i].Select(r =>
                                                                        r.Severity.Value).Max()).ToLower();
                }
                else
                {
                    dayColor = SD.COLOR_BLUE;
                }
                output[i] = dayColor.ToLower();
            }
            return output;
        }
    }
}
