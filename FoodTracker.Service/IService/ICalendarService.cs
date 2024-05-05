using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;

namespace FoodTracker.Service.IService
{
    public interface ICalendarService
    {

        DayVM[,] GetPopulatedCalendarDays(
                            Dictionary<string, List<Meal>> mealDict, 
                            Dictionary<string, List<Reaction>> reactionDict, 
                            Dictionary<string, Icon> iconDict, 
                            Dictionary<string, bool> userSafeDays, 
                            DateTime dt);

        string GetDayColor(DateTime day);
        public Dictionary<int, string> GetDayColorByMonth(DateTime date, Dictionary<int, List<Reaction>> reactions);

    }
}
