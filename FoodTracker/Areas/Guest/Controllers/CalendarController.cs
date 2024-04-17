using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class CalendarController(IUnitOfWork unitOfwork) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        private readonly List<DayVM> _calendarDays = new();
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        public IActionResult Index()
        {


            var mealDict = new Dictionary<string, List<Meal>>(); 
            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == Helper.GetAppUserId(User), includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME, Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR]);

            foreach (var meal in meals)
            {
                var tempList = new List<Meal>();
                var dateKey = meal.DateTime.ToString("yyyy-MM-dd").Substring(0, 10);
                if (!mealDict.TryGetValue(dateKey, out tempList))
                {
                    mealDict[dateKey] = [];
                }

                mealDict[dateKey].Add(meal);
            }

            DateTime dt = DateTime.Now;

            var month = dt.Month;
            var year = dt.Year;
            var day = dt.Day;

            var testMonth = month;
            var testYear = year;
            var firstDayOfMonth = new DateTime(testYear, testMonth, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            // how long is month
            var daysInMonth = DateTime.DaysInMonth(testYear, testMonth);

            // how many weeks to show
            var rowsNeeded = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;



            CalendarVM = new()
            {
                Daysb = new string[rowsNeeded][]
            };
           
            var daysj = new DayVM[rowsNeeded, 7];

            int dayIndex = 0 - firstDayOfMonthIndex;
            for (int row = 0; row < rowsNeeded; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DayVM newDay = new();

                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        newDay.Day = null;
                        daysj[row, col] = newDay;
                    }
                    else
                    {
                        // if exists in dict, add to day as dayVM
                        var dayKey = $"{testYear}-{testMonth:D2}-{dayIndex + 1:D2}";
                        Console.WriteLine(dayKey);
                        var today = new DateTime(dt.Year, dt.Month, dayIndex + 1);
                        Console.WriteLine(today.ToString("yyyy-MM-dd"));





                        //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
                        //var dayMeals = new List<Meal>();
                        
                        if (mealDict.TryGetValue(dayKey, out List<Meal>? dayMeals))
                        {
                            newDay.Meals = dayMeals;
                        }
                        else
                        {
                            newDay.Meals = [];
                        }
                        
                        //newDay.DateTime = new DateTime(testYear, testMonth, dayIndex + 1);
                        newDay.DateTime = today;
                        newDay.Day = dayIndex + 1;
                        newDay.Color = new Color { Name = "Red" };

                        daysj[row, col] = newDay;
                    }

                    _calendarDays.Add(newDay);
                    dayIndex++;
                }
            }
            CalendarVM.DaysJ = daysj;

            return View(CalendarVM);

        }

        [HttpPost]
        public IActionResult Index(MealVM mealVM)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            //return RedirectToAction("Index");
            var updatedMeal = mealVM.Meal;
            var appUserId = Helper.GetAppUserId(User);

            // verify mealId is for user
            var verifiedMeal = _unitOfWork.Meal.Get(m => m.Id == updatedMeal.Id && m.AppUserId == appUserId, includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);


            // get and then delete all mealItems with that meal id
            if (verifiedMeal != null && verifiedMeal.MealItems.Count > 0) 
            {
                _unitOfWork.MealItem.RemoveRange(verifiedMeal.MealItems.ToList());
            }

            var verifiedMealItemsList = new List<MealItem>();
            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == appUserId || f.Global).Select(f => f.Id).ToList();

            foreach (var (key, mealItem) in mealVM.MealItems)
            {
                if (validUserFoods.Contains(mealItem.FoodId))
                {
                    verifiedMealItemsList.Add(mealItem);
                }
            }

            // get and then delete all mealReactions with that meal id
            if (verifiedMeal != null && verifiedMeal.Reactions.Count > 0)
            {
                _unitOfWork.Reaction.RemoveRange(verifiedMeal.Reactions.ToList());
            }

            var verifiedMealReactionsList = new List<Reaction>();
                     
            foreach(var (reactionId, _) in mealVM.Reactions)
            {
                verifiedMealReactionsList.Add(
                    new Reaction
                    {
                        AppUserId = appUserId,
                        SourceTypeId = ReactionSource.Meal,
                        TypeId = reactionId
                    });
            }
            
            updatedMeal.AppUserId = appUserId;
            updatedMeal.MealItems = verifiedMealItemsList;
            updatedMeal.Reactions = verifiedMealReactionsList;

            if (updatedMeal.Id == 0)
            {
                _unitOfWork.Meal.Add(updatedMeal);
            }
            else
            {
                _unitOfWork.Meal.Update(updatedMeal);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpsertMeal([FromBody] DayVM dayVM)
        {
                      
            DateTime mealTime = dayVM.DateTime.Date == DateTime.Now.Date 
                                    ? mealTime = DateTime.Now 
                                    : mealTime = dayVM.DateTime.Date.AddHours(12);
           
            Meal? activeMeal = null;

            var userId = Helper.GetAppUserId(User);

            var priorReactions = new Dictionary<int, bool>();
            if (dayVM.ActiveMealId != 0)
            {
                activeMeal = _unitOfWork.Meal.Get(m => m.AppUserId == userId && m.Id == dayVM.ActiveMealId, includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

                foreach (var reaction in activeMeal.Reactions)
                {
                    priorReactions[reaction.Type.Id] = true;
                    reaction.AppUserId = null;
                }
                activeMeal.Reactions = null;
            }



            activeMeal ??= new Meal() { DateTime = mealTime };

            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);

            MealVM = new()
            {
                ColorOptions = _unitOfWork.Color.GetAll(),
                //Categories = Helper.GetReactionDict(_unitOfWork),
                Reactions = priorReactions,
                Categories = Helper.GetReactionDict(reactions),
                Meal = activeMeal,
                Foods = _unitOfWork.Food.GetAll(f => f.AppUserId == userId || f.Global).OrderBy(x => x.Name),
                MealTypes = _unitOfWork.MealType.GetAll(),
                Units = _unitOfWork.Unit.GetAll(u => u.Type == 1)
            };

            return PartialView("_AddMealPartial", MealVM);
        }

        [HttpDelete]
        public IActionResult RemoveMeal(int id)
        {
            var mealToDelete = _unitOfWork.Meal.Get(m => m.Id == id && m.AppUserId == Helper.GetAppUserId(User));

            if (mealToDelete != null)
            {
                _unitOfWork.Meal.Remove(mealToDelete);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
