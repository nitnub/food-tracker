using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using System.Collections;
using System.Security.Claims;

namespace FoodTracker.Utility
{
    public class Helper
    {


        public string GetMaxMealSeverityColorString(Meal? meal)
        {
            if (meal == null || meal.MealItems == null)
                return "";

            var foodColors = new List<string>();

            foreach (var mealItem in meal.MealItems)
            {
                var foodColor = GetMaxSeverityColorString(mealItem.Food);

                //if (foodCol)
                //foodColors.Add()
            }
            return "";
         }





        //public static Dictionary<int, Dictionary<int, int>> GetFoodTypeSeverityDict(IUnitOfWork unitOfWork, string userId )
        public static Dictionary<int, Dictionary<int, int>> GetFoodTypeSeverityDict(IEnumerable<Reaction> existingReactions)
        {

            //var existingReactions = unitOfWork.Reaction.GetAll(u => u.AppUserId == userId);
            var foodTypeSeverityDict = new Dictionary<int, Dictionary<int, int>>();

            foreach (var reaction in existingReactions)
            {

                if (reaction.FoodId == null) continue;

                var foodId = (int)reaction.FoodId;
                var reactionTypeId = (int)reaction.TypeId;
                var reactionSeverityId = (int)reaction.SeverityId;


                var reactionTypeSeverityDict = new Dictionary<int, int>();
                if (foodTypeSeverityDict.TryGetValue(foodId, out reactionTypeSeverityDict))
                {
                    reactionTypeSeverityDict[reactionTypeId] = reactionSeverityId;
                }
                else
                {
                    foodTypeSeverityDict[foodId] = [];
                    foodTypeSeverityDict[foodId][reactionTypeId] = reactionSeverityId;
                }
            }

            return foodTypeSeverityDict;
        }

        //public static Dictionary<string, List<ReactionType>> GetReactionDict(IUnitOfWork unitOfWork)
        public static Dictionary<string, List<ReactionType>> GetReactionDict(IEnumerable<ReactionType> reactions)
        {
            //var reactions = unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            
            var reactionDict = new Dictionary<string, List<ReactionType>>();
            var categories = new List<ReactionType>();

            foreach (var reaction in reactions)
            {
                var category = reaction.Category.Name;

                if (reactionDict.TryGetValue(category, out categories))
                {
                    categories.Add(reaction);
                }
                else
                {
                    reactionDict[category] = [];
                    reactionDict[category].Add(reaction);
                }
            }

            return reactionDict;
        }

        public static string GetMaxSeverityColorString(Food? food)
        {
            if (food == null || food.Reactions == null)
                return "";

            if (food.UserSafeFoods?.Count() > 0)
                return SD.COLOR_GREEN;

            var maxSeverity = food.Reactions?
                                        .Select(r => r.Severity.Value)
                                        .DefaultIfEmpty(-1)
                                        .Max() ?? -1;

            return maxSeverity switch
            {
                <= 1 => "",
                <= 5 => SD.COLOR_YELLOW,
                _ => SD.COLOR_RED
            };
        }

        public static string GetMaxKnownProductFodColorString(ArrayList items)
        {
            var maxFodColor = SD.COLOR_BLUE;
            Food food;
            string color;

            foreach (var item in items)
            {
                if (item.GetType() != typeof(FoodVM))
                    continue;

                food = ((FoodVM)item).Food;

                if (food.Fodmap == null)
                    continue;

                color = food.Fodmap.Color.Name;

                if (color == SD.COLOR_RED)
                    return SD.COLOR_RED;

                if (color == SD.COLOR_YELLOW)
                    maxFodColor = SD.COLOR_YELLOW;
            }
            return maxFodColor;
        }

        public static string? GetAppUserId(ClaimsPrincipal User)
        {
            ClaimsIdentity claimsIdentity;
            Claim? nameIdentifier;

            if (User.Identity == null)
                return null;

            claimsIdentity = (ClaimsIdentity)User.Identity;
            nameIdentifier = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifier == null)
                return null;

            return nameIdentifier.Value;
        }
    }
}
