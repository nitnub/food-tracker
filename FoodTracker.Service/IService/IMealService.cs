using FoodTracker.Models.IModel;
using FoodTracker.Models.Meal;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodTracker.Service.IService
{
    public interface IMealService
    {
        Dictionary<string, List<Meal>> GetDateMealDict(string dateFormat = SD.DATE_FORMAT);
        public List<MealItem> GetValidatedMealItemsList(List<MealItem> unverifiedMeals);
        bool Remove(int? id);
        bool RemoveTemplate(int? id);
        bool Upsert(Meal meal, List<MealItem> mealItems, List<int> reactionIds);
        List<Meal> GetMealsByDate(DateTime datetime);
        Dictionary<int, List<Meal>> GetMealsByMonth(DateTime datetime);
        Dictionary<int, List<Meal>> GetMealsForSurroundingMonths(DateTime datetime);
        Meal GetMealDetails(int id);
        Dictionary<int, bool> GetMealReactionDict(Meal activeMeal);
        Meal CreateBlankMeal(DateTime mealTime);
        IEnumerable<MealType> GetAllMealTypes();
        IEnumerable<SelectListItem> GetMealTemplateOptions();
        Meal GetTemplateMeal(int id);
        int GetMatchingMealTemplateId(Meal meal);
    }
}
