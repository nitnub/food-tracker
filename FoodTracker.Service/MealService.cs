using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Meal;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

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
            var meal = _unitOfWork.Meal.Get(m => m.AppUserId == UserId && m.Id == id, includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

            foreach (var reaction in meal.Reactions)
            {
                reaction.AppUserId = null;
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

        public bool Upsert(Meal meal, List<MealItem> mealItems, List<int> reactionIds)
        {
            var success = false;

            try
            {
                meal.AppUserId = UserId;
                meal.MealItems = GetValidatedMealItemsList(mealItems);
                meal.Reactions = _reactionService.CreateMealReactionsList(reactionIds);

                if (meal.Id == 0)
                {
                    _unitOfWork.Meal.Add(meal);
                }
                else
                {
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
                // verify mealId is for user
                var verifiedMeal = _unitOfWork.Meal.Get(m => m.Id == id && m.AppUserId == UserId,
                                                        includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

                if (verifiedMeal == null)
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
        public List<MealItem> GetValidatedMealItemsList(List<MealItem> unverifiedMeals)
        {
            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == UserId || f.Global)
                                            .Select(f => f.Id).ToList();

            return unverifiedMeals.Where(mi => validUserFoods.Contains(mi.FoodId)).ToList();
        }

        public IEnumerable<MealType> GetAllMealTypes()
        {
            return _unitOfWork.MealType.GetAll();
        }
    }

}
