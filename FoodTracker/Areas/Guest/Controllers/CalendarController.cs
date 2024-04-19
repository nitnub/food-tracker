using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class CalendarController(IUnitOfWork unitOfwork) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        public IActionResult Index(CalendarVM vm)
        {

            var userId = Helper.GetAppUserId(User);
            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == userId,
                            includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME, Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR]);

            var mealDict = meals.GroupBy(m => m.DateTime.ToString("yyyy-MM-dd"))
                                .ToDictionary(m => m.Key, m => m.ToList());

            DateTime dt = vm.ViewDate.Year > 1 ? vm.ViewDate : DateTime.Now;

            CalendarVM = new()
            {
                ViewYear = dt.Year,
                ViewMonth = dt.Month,
                ViewDay = dt.Day,
                ViewDate = dt,
                DayVMs = GetPopulatedCalendarDays(mealDict, dt)
            };

            return View(CalendarVM);
        }

        #region API 
        [HttpPost]
        public IActionResult PriorYear(CalendarVM vm)
        {
                return RediretToUpdatedCalendar(vm.ViewYear - 1, vm.ViewMonth);
        }

        [HttpPost]
        public IActionResult NextYear(CalendarVM vm)
        {
                return RediretToUpdatedCalendar(vm.ViewYear + 1, vm.ViewMonth);
        }

        [HttpPost]
        public IActionResult PriorMonth(CalendarVM vm)
        {
                return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth - 1);
        }

        [HttpPost]
        public IActionResult NextMonth(CalendarVM vm)
        {
                return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth + 1);
        }

        [HttpPost]
        public IActionResult JumpToDate(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth);
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

            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == appUserId || f.Global).Select(f => f.Id).ToList();

            //var verifiedMealItemsList = new List<MealItem>();
            //foreach (var (key, mealItem) in mealVM.MealItems)
            //{
            //    if (validUserFoods.Contains(mealItem.FoodId))
            //    {
            //        verifiedMealItemsList.Add(mealItem);
            //    }
            //}

            var verifiedMealItemsList = mealVM.MealItems.Values
                                            .Where(mi => validUserFoods.Contains(mi.FoodId))
                                            .ToList();

            // get and then delete all mealReactions with that meal id
            if (verifiedMeal != null && verifiedMeal.Reactions.Count > 0)
            {
                _unitOfWork.Reaction.RemoveRange(verifiedMeal.Reactions.ToList());
            }

            var verifiedMealReactionsList = new List<Reaction>();

            foreach (var (reactionId, _) in mealVM.Reactions)
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

        private static DayVM[,] GetPopulatedCalendarDays(Dictionary<string, List<Meal>> mealDict, DateTime dt)
        {
            var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            // how long is month
            var daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);

            // how many weeks to show
            var weeksInMonth = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;

            var dayVMs = new DayVM[weeksInMonth, 7];
            int dayIndex = 0 - firstDayOfMonthIndex;

            for (int row = 0; row < weeksInMonth; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DayVM newDay = new();

                    // If day falls outside of current month...
                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        newDay.Day = null;
                    }
                    else
                    {
                        var today = new DateTime(dt.Year, dt.Month, dayIndex + 1);
                        var dayKey = today.ToString("yyyy-MM-dd");

                        if (mealDict.TryGetValue(dayKey, out List<Meal>? dayMeals))
                        {
                    

                            newDay.Meals = dayMeals;
                        }
                        else
                        {
                            newDay.Meals = [];
                        }

                        newDay.DateTime = today;
                        newDay.Day = dayIndex + 1;
                        newDay.Color = new Color { Name = "Red" };

                    }
                    dayVMs[row, col] = newDay;

                    dayIndex++;
                }
            }

            return dayVMs;
        }

        #endregion

        private RedirectToActionResult RediretToUpdatedCalendar(int year, int month = 1, int day = 1)
        {
            try
            {
                var newDate = new DateTime(year, month, day);
                var calendarVM = new CalendarVM
                {
                    ViewDate = newDate
                };

                return RedirectToAction(nameof(Index), calendarVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }
    }
}
