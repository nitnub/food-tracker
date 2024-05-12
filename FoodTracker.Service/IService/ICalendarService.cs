using FoodTracker.Models.Reaction;

namespace FoodTracker.Service.IService
{
    public interface ICalendarService
    {
        string GetDayColor(DateTime day);
        public Dictionary<int, string> GetDayColorByMonth(DateTime date, Dictionary<int, List<Reaction>> reactions);
        public Dictionary<int, string> GetDayColorsForSurroundingMonths(DateTime date, Dictionary<int, List<Reaction>> reactions);

    }
}
