using FoodTracker.Models.Day;
using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Utility;

namespace FoodTracker.Service.IService
{
    public interface IReactionService
    {
        IEnumerable<Reaction> GetAll();
        List<Reaction> GetAllDayReactions(DateTime dateTime);
        IEnumerable<ReactionSeverity> GetAllSeverities();
        Dictionary<string, List<ReactionType>> GetReactionCategoryDict();
        Dictionary<string, List<Reaction>> GetDayReactionDict(string dateFormat = SD.DATE_FORMAT);
        Dictionary<int, List<Reaction>> GetAllDayReactionsForTheMonth(DateTime dateTime);
        Dictionary<int, Dictionary<int, int>> GetExistingReactionSeveritiesDict();

        Dictionary<DateTime, Dictionary<int, int>> GetDayTypeSeverityDict(DateTime dateTime);
        Dictionary<string, bool> GetUserSafeDaysDict(DateTime dt, string dateFormat = SD.DATE_FORMAT);
        string ToggleReaction(Reaction reaction);
        bool ToggleDayReaction(Reaction reaction);
        bool ToggleUserSafeFood(int id);
        bool ToggleUserSafeDay(DateTime date);

        string GetMaxSeverityColorString(Food food);
        string GetMaxSeverityColorString(int foodId);
        string GetDayColorString(DateTime day);

        bool IsUserSafeDay(DateTime dateTime);

        List<Reaction> CreateMealReactionsList(List<int> reactionIds);
        List<Reaction> CreateMealReactionsList(Dictionary<int, bool> reactionDict);
        List<ReactionIcon> GetActiveIcons(DateTime date);
        Dictionary<int, List<ReactionIcon>> GetActiveIcons(int year, int month);
        ReactionIcon GetUserSafeDayIcon();
        ReactionIcon GetNeutralDayIcon();
    }
}
