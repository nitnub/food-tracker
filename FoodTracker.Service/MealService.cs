using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.IModel;
using FoodTracker.Models.Meal;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodTracker.Service
{
    public class MealService(string userId, IUnitOfWork unitOfWork) : IMealService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private string UserId { get; } = userId;
        private readonly ReactionService _reactionService = new(userId, unitOfWork);

        public Dictionary<string, List<Meal>> GetDateMealDict(string dateFormat = SD.DATE_FORMAT)
        {
            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == userId,
                                                 includeProperties: [
                                                     Prop.COLOR,
                                                     Prop.MEAL_ITEMS_FOOD,
                                                     Prop.MEAL_ITEMS_VOLUME,
                                                     Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR]);
            
            var mealDict = meals.GroupBy(m => m.DateTime.ToString(dateFormat))
                                                .ToDictionary(m => m.Key, m => m.ToList());

            return mealDict;
        }

        public Meal GetMealDetails(int id)
        {
            var meal = _unitOfWork.Meal.Get(m => m.AppUserId == UserId && m.Id == id, includeProperties: [Prop.MEAL_ITEMS,  Prop.REACTIONS_TYPE]);

            foreach (var reaction in meal.Reactions)
            {
                reaction.AppUserId = null;
                reaction.Meal = null;
            }

            if (meal.IsTemplate) // if adding or upserting chekc for template in two workflows
            {
                //meal.Id = 0;
                //foreach (var mealItem in meal.MealItems)
                //{
                //    mealItem.MealId = 0;
                //    mealItem.Id = 0;
                //}
            }

            return meal;
        }

        public Dictionary<int, bool> GetMealReactionDict(Meal activeMeal)
        {
            var priorReactions = new Dictionary<int, bool>();

            foreach (var reaction in activeMeal.Reactions)
            {
                priorReactions[reaction.Type.Id] = true;
            }
            return priorReactions;
        }

        public Meal CreateBlankMeal(DateTime mealTime)
        {
            return new Meal() { DateTime = mealTime, MealItems = [new MealItem()] };
        }

        public List<Meal> GetMealsByDate(DateTime dateTime)
        {
            return _unitOfWork.Meal.GetAll(m => m.AppUserId == UserId &&
                                                m.DateTime.Date == dateTime.Date,
                                                includeProperties: [
                                                    Prop.COLOR,
                                                    Prop.MEAL_ITEMS_FOOD,
                                                    Prop.MEAL_ITEMS_VOLUME,
                                                    Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR])
                                                .ToList();
        }

        public Dictionary<int, List<Meal>> GetMealsForSurroundingMonths(DateTime dateTime)
        {
            var dh = new DateHelper(dateTime);
            var padLast = dh.GetLastMonthPad();
            var padNext = dh.GetNextMonthPad();

            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == UserId &&
                                                m.DateTime >= padLast &&
                                                m.DateTime <= padNext,
                                                includeProperties: [
                                                    Prop.COLOR,
                                                    Prop.MEAL_ITEMS_FOOD,
                                                    Prop.MEAL_ITEMS_VOLUME,
                                                    Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR])
                                                .GroupBy(m => m.DateTime.DayOfYear - dh.FirstDayOfMonth.DayOfYear)
                                                .ToDictionary(m => m.Key, m => m.ToList());

            for (int i = -7; i <= daysInMonth + 7; i++)
            {
                if (!meals.ContainsKey(i))
                {
                    meals[i] = [];
                }
            }
            return meals;
        }

        public Dictionary<int, List<Meal>> GetMealsByMonth(DateTime dateTime)
        {
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == UserId &&
                                                m.DateTime.Date.Year == dateTime.Date.Year &&
                                                m.DateTime.Date.Month == dateTime.Date.Month,
                                                includeProperties: [
                                                    Prop.COLOR,
                                                    Prop.MEAL_ITEMS_FOOD,
                                                    Prop.MEAL_ITEMS_VOLUME,
                                                    Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR])
                                                .GroupBy(m => m.DateTime.Date.Day)
                                                .ToDictionary(m => m.Key, m => m.ToList());

            for (int i = 1; i <= daysInMonth; i++)
            {
                if (!meals.ContainsKey(i))
                {
                    meals[i] = [];
                }
            }
            return meals;
        }

        public bool Upsert(Meal meal, List<MealItem> mealItems, List<int> reactionIds)
        {
            var success = false;
            try
            {
                if (meal.IsGlobal)
                    return success;

                meal.AppUserId = UserId;
                meal.MealItems = GetValidatedMealItemsList(mealItems);
                meal.Reactions = _reactionService.CreateMealReactionsList(reactionIds);

                if (meal.Id == 0)
                {
                    _unitOfWork.Meal.Add(meal);
                }
                else
                {
                    var verifiedMeal = _unitOfWork.Meal.Get(m => m.AppUserId == UserId && m.Id == meal.Id);

                    if (verifiedMeal == null)
                        return success;

                    var mealItemsToRemove = _unitOfWork.MealItem.GetAll(mi => mi.MealId == verifiedMeal.Id).ToList();
                    
                    _unitOfWork.MealItem.RemoveRange(mealItemsToRemove);
                    _unitOfWork.Meal.Update(meal);
                }

                _unitOfWork.Save();

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return success;
        }
        public bool Remove(int? id)
        {
            var success = false;
            if (id != null && id != 0)
            {
                var verifiedMeal = _unitOfWork.Meal.Get(m => m.Id == id && m.AppUserId == UserId,
                                                        includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

                if (verifiedMeal == null || verifiedMeal.AppUserId != UserId || verifiedMeal.IsGlobal)
                    return success;

                _unitOfWork.Meal.Remove(verifiedMeal);

                if (verifiedMeal != null && verifiedMeal.Reactions.Count > 0)
                {
                    _unitOfWork.Reaction.RemoveRange(verifiedMeal.Reactions.ToList());
                }
                _unitOfWork.Save();
            }

            return success;
        }

        public bool RemoveTemplate(int? id)
        {
            return false;
        }
        public List<MealItem> GetValidatedMealItemsList(List<MealItem> unverifiedMeals)
        {
            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == UserId || f.IsGlobal)
                                            .Select(f => f.Id).ToList();

            return unverifiedMeals.Where(mi => validUserFoods.Contains(mi.FoodId)).ToList();
        }

        public IEnumerable<MealType> GetAllMealTypes()
        {
            return _unitOfWork.MealType.GetAll();
        }

        public IEnumerable<SelectListItem> GetMealTemplateOptions()
        {

            // get all where marked as template and either userID or global 
            var personalGroup = new SelectListGroup() { Name = "Personal" };
            var globalGroup = new SelectListGroup() { Name = "Global" };

            return _unitOfWork.Meal.GetAll(m => (m.AppUserId == UserId || m.IsGlobal) && m.IsTemplate)
                                   .Select(m => new SelectListItem() { 
                                       Value = m.Id.ToString(), 
                                       Text = m.Name, 
                                       Group = m.IsGlobal ? globalGroup : personalGroup });

        }


        public Meal GetTemplateMeal(int id)
        {
            
            var templateMeal = _unitOfWork.Meal.Get(m => m.Id == id && 
                                                        (m.AppUserId == UserId || m.IsGlobal) &&
                                                        m.IsTemplate);



            return templateMeal;
        }

        public int GetMatchingMealTemplateId(Meal meal)
        {
            int id = 0;
            var result = _unitOfWork.Meal.Get(m =>
                            m.AppUserId == meal.AppUserId &&
                            m.IsTemplate == true &&
                            m.Name == meal.Name &&
                            m.ColorId == meal.ColorId &&
                            m.MealTypeId == meal.MealTypeId
                            );

            if (result != null)
            {
                id = result.Id;
            }
            return id;
        }
    }
}
