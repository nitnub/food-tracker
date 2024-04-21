using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class CalendarController(IUnitOfWork unitOfwork) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        public DayReactionVM DayReactionVM { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        public IActionResult Index(CalendarVM vm)
        {

            var userId = Helper.GetAppUserId(User);
            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == userId,
                            includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME, Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR]);
            var dayReactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == userId && r.SourceTypeId == ReactionSource.Day,
                            includeProperties: [Prop.SEVERITY, Prop.TYPE_CATEGORY_ICON]);

            var mealDict = meals.GroupBy(m => m.DateTime.ToString("yyyy-MM-dd"))
                                .ToDictionary(m => m.Key, m => m.ToList());

            var reactionDict = dayReactions.GroupBy(r => r.IdentifiedOn.Value.ToString("yyyy-MM-dd"))
                                .ToDictionary(r => r.Key, r => r.ToList());

            DateTime dt = vm.ViewDate.Year > 1 ? vm.ViewDate : DateTime.Now;





            #region Reaction Controller Test

            var dateTime = DateTime.Now;

            //get all reactions for this day
            var appUserId = Helper.GetAppUserId(User);


            var todaysReactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == appUserId && r.SourceTypeId == ReactionSource.Day && r.IdentifiedOn == dateTime);

            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            var reactionDictForVm = Helper.GetReactionDict(reactions);
            var dayTypeSeverityDict = Helper.GetDayTypeSeverityDict(todaysReactions);
            // get all reaction types and severities
            // populate t&S dictionaries

            DayReactionVM = new()
            {
                DateTime = DateTime.Now,
                Categories = reactionDictForVm,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                ExistingReactions = Helper.GetDayTypeSeverityDict(todaysReactions)
            };





            #endregion




            CalendarVM = new()
            {
                ViewYear = dt.Year,
                ViewMonth = dt.Month,
                ViewDay = dt.Day,
                ViewDate = dt,
                DayVMs = GetPopulatedCalendarDays(mealDict, reactionDict, dt),
                DayReactionVM = DayReactionVM
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

            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == appUserId || f.Global)
                                                 .Select(f => f.Id).ToList();

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

            activeMeal ??= new Meal() { DateTime = mealTime, MealItems = [new MealItem()] };
 
            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);

            MealVM = new()
            {
                ColorOptions = _unitOfWork.Color.GetAll(),
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
    
        [HttpGet]
        public IActionResult GetDayReactions(DateTime dateTime)
        {


            //var dateTime = DateTime.Now;
            //get all reactions for this day
            var appUserId = Helper.GetAppUserId(User);


            var todaysReactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == appUserId && r.SourceTypeId == ReactionSource.Day && r.IdentifiedOn == dateTime);

            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            var reactionDict = Helper.GetReactionDict(reactions);
            var dayTypeSeverityDict = Helper.GetDayTypeSeverityDict(todaysReactions);
            // get all reaction types and severities
            // populate t&S dictionaries

            DayReactionVM = new()
            {
                DateTime = dateTime,
                Categories = reactionDict,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                ExistingReactions = Helper.GetDayTypeSeverityDict(todaysReactions)
            };

            return PartialView("_AddDayReactionPartial", DayReactionVM);
        }


        [HttpPost]
        public IActionResult ToggleDayReaction([FromBody] Reaction r)
        {
            var userId = Helper.GetAppUserId(User);
            var success = false;
            Food? food = null;

            r.SourceTypeId = ReactionSource.Day;
            //r.SourceTypeId = ReactionSourceType;

            if (ModelState.IsValid && userId != null)
            {
                r.AppUserId = userId;

                if (!QueueRemovalOfRelatedDayReactions(_unitOfWork, r))
                    _unitOfWork.Reaction.Add(r);
                _unitOfWork.Save();

                //food = _unitOfWork.Food.Get(f => f.Id == r.FoodId &&
                //            (f.AppUserId == userId || f.Global),
                //            includeProperties: Prop.REACTIONS_SEVERITY);

                //success = food != null;
                success = true;
            }

            //return Json(new { success, updatedColor = Helper.GetMaxSeverityColorString(food).ToLower() });
            return Json(new { success, updatedColor = "Red" });
        }
        #endregion

        private static DayVM[,] GetPopulatedCalendarDays(Dictionary<string, List<Meal>> mealDict, Dictionary<string, List<Reaction>> reactionDict, DateTime dt)
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

                        if (reactionDict.TryGetValue(dayKey, out List<Reaction>? dayReactions))
                        {
                            newDay.Reactions = dayReactions;
                        }
                        else
                        {
                            newDay.Reactions = [];
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

        private static bool QueueRemovalOfRelatedDayReactions(IUnitOfWork unitOfWork, Reaction newReaction)
        {
            var success = false;
            var reactionToRemove = unitOfWork.Reaction.Get(r =>
                                            r.SourceTypeId == ReactionSource.Day &&
                                            r.IdentifiedOn.Value.Date == newReaction.IdentifiedOn.Value.Date &&
                                            r.TypeId == newReaction.TypeId &&
                                            r.AppUserId == newReaction.AppUserId);

            if (reactionToRemove != null)
            {
                unitOfWork.Reaction.Remove(reactionToRemove);
                success = reactionToRemove.SourceTypeId == ReactionSource.Day &&
                                            reactionToRemove.IdentifiedOn.Value.Date == newReaction.IdentifiedOn.Value.Date &&
                                            reactionToRemove.TypeId == newReaction.TypeId &&
                                            reactionToRemove.SeverityId == newReaction.SeverityId;
            }

            return success;
        }
    }
}
