using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Month = FoodTracker.Utility.Month;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    [Authorize]
    public class CalendarController(IUnitOfWork unitOfwork, IServiceManager serviceManager) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        public DayReactionVM DayReactionVM { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        private readonly ICalendarService _calendarService = serviceManager.Calendar;
        private readonly IFoodService _foodService = serviceManager.Food;
        private readonly IMealService _mealService = serviceManager.Meal;
        private readonly IReactionService _reactionService = serviceManager.Reaction;
        private readonly IUtilityService _utilityService = serviceManager.Utility;
        private readonly IActivityService _activityService = serviceManager.Activity;
        public IActionResult Index(CalendarVM vm)
        {
            DateTime dt = vm.ViewDate.Year > 1 ? vm.ViewDate : DateTime.Now;

            CalendarVM = new()
            {
                ViewYear = dt.Year,
                ViewMonth = dt.Month,
                ViewDay = dt.Day,
                ViewDate = dt,
                DayVMs = GetPopulatedCalendarDays(dt),
                ReactionIcons = _utilityService.GetReactionIconDict(),
                DateShortString = $"{(Month)(DateTime.Now.Date.Month - 1)}, {DateTime.Now.Date.Year}"
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

            var year = vm.ViewMonth == 1 ? vm.ViewYear - 1 : vm.ViewYear;
            var month = vm.ViewMonth == 1 ? 12 : vm.ViewMonth - 1;

            return RediretToUpdatedCalendar(year, month);
        }

        [HttpPost]
        public IActionResult NextMonth(CalendarVM vm)
        {

            var year = vm.ViewMonth == 12 ? vm.ViewYear + 1 : vm.ViewYear;
            var month = vm.ViewMonth == 12 ? 1 : vm.ViewMonth + 1;

            return RediretToUpdatedCalendar(year, month);
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

            mealVM.Reactions ??= [];

            var updatedMeal = mealVM.Meal;
            var mealItems = mealVM.MealItems.Values.ToList();
            var reactionIds = mealVM.Reactions.Keys.ToList();

            _mealService.Upsert(updatedMeal, mealItems, reactionIds);

            CalendarVM = new() { ViewDate = mealVM.Meal.DateTime };

            return RedirectToAction("Index", CalendarVM);
        }

        [HttpPost]
        public IActionResult UpsertMeal([FromBody] DayVM dayVM)
        {

            DateTime mealTime = dayVM.DateTime.Date == DateTime.Now.Date
                                    ? DateTime.Now
                                    : dayVM.DateTime.Date.AddHours(12);

            Meal? activeMeal = null;

            var priorReactions = new Dictionary<int, bool>();
            if (dayVM.ActiveMealId != 0)
            {
                activeMeal = _mealService.GetMealDetails(dayVM.ActiveMealId);
                priorReactions = _mealService.GetMealReactionDict(activeMeal);
                activeMeal.Reactions = null;
            }

            activeMeal ??= _mealService.CreateBlankMeal(mealTime);

            MealVM = new()
            {
                ColorOptions = _utilityService.GetAllColors(),
                Reactions = priorReactions,
                Categories = _reactionService.GetReactionCategoryDict(),
                Meal = activeMeal,
                Foods = _foodService.GetAllSorted(),
                MealTypes = _mealService.GetAllMealTypes(),
                Units = _utilityService.GetAllVolumeUnits()
            };

            return PartialView("_AddMealPartial", MealVM);

        }

        [HttpDelete]
        public IActionResult RemoveMeal(int id)
        {
            _mealService.Remove(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetDayReactions(DateTime dateTime)
        {
            DayReactionVM = new()
            {
                DateTime = dateTime,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                Categories = _reactionService.GetReactionCategoryDict(),
                ExistingReactions = _reactionService.GetDayTypeSeverityDict(dateTime),
                UserSafe = _reactionService.IsUserSafeDay(dateTime)
            };

            return PartialView("_AddDayReactionPartial", DayReactionVM);
        }


        [HttpPost]
        public IActionResult ToggleDayReaction([FromBody] Reaction reaction)
        {
            var userId = Helper.GetAppUserId(User);
            var success = false;
            var dayColor = "";
            var activeIcons = new List<ReactionIcon>();
            var isUserSafeDay = _reactionService.IsUserSafeDay((DateTime)reaction.IdentifiedOn);

            if (!isUserSafeDay && ModelState.IsValid && userId != null)
            {
                success = _reactionService.ToggleDayReaction(reaction);
                activeIcons = _reactionService.GetActiveIcons(reaction.IdentifiedOn.Value.Date);
                dayColor = _calendarService.GetDayColor(reaction.IdentifiedOn.Value);
            }
            return Json(new { success, isUserSafeDay, activeIcons, dayColor });
        }

        [HttpPost]
        public IActionResult UpdateUserSafeDay(DateTime date)
        {
            var active = false;
            var success = false;
            var message = "Error updating user's safe days";
            var userId = Helper.GetAppUserId(User);
            var updatedColor = "";
            var reactionIcons = new List<ReactionIcon>();

            if (userId == null)
                message = "Unable to find user";

            else if (date.Date.Year == DateOnly.MinValue.Year)
                message = "Unable to find date";

            else
            {
                if (_reactionService.ToggleUserSafeDay(date))
                {
                    reactionIcons.Add(_reactionService.GetUserSafeDayIcon());
                    updatedColor = SD.COLOR_GREEN;
                    active = true;
                }
                else
                {
                    reactionIcons = _reactionService.GetActiveIcons(date);
                    updatedColor = _reactionService.GetDayColorString(date);
                }

                success = true;
                message = "Safe days updated";
                _unitOfWork.Save();
            }
            return Json(new { success, active, message, updatedColor, reactionIcons });
        }

        #endregion

        private DayVM[,] GetPopulatedCalendarDays(DateTime dt)
        {
            var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            var daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            var weeksInMonth = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;

            var dayVMs = new DayVM[weeksInMonth, 7];
            int dayIndex = 0 - firstDayOfMonthIndex;

            var activityDict = _activityService.GetMonthActivitiesDict(dt);
            var reactionIcons = _reactionService.GetActiveIcons(dt.Year, dt.Month);
            var mealIcons = _mealService.GetMealsByMonth(dt);

            var dayReactions = _reactionService.GetAllDayReactionsForTheMonth(dt);
            var dayColors = _calendarService.GetDayColorByMonth(dt, dayReactions);
            for (int row = 0; row < weeksInMonth; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DayVM newDay; // = new();

                    // If day falls outside of current month...
                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        newDay = new()
                        {
                            Day = null,
                            ReactionIcons = [],
                            ActivityIcons = []
                        };
                        //newDay.Day = null;
                        //newDay.ReactionIcons = [];
                        //newDay.ActivityIcons = [];


                    }
                    // else if day is in current month...
                    else
                    {
                        var today = new DateTime(dt.Year, dt.Month, dayIndex + 1);

                        newDay = new()
                        {
                            Meals = mealIcons[today.Day],
                            Reactions = dayReactions[today.Day],
                            ReactionIcons = reactionIcons[today.Day],
                            DateTime = today,
                            Day = dayIndex + 1,
                            Color = dayColors[today.Day]
                        };

                        //newDay.Meals = mealIcons[today.Day];
                        //newDay.Reactions = dayReactions[today.Day];
                        //newDay.ReactionIcons = reactionIcons[today.Day];


                        //newDay.DateTime = today;
                        //newDay.Day = dayIndex + 1;
                        //newDay.Color = dayColors[today.Day];


                        List<Icon> dayActivities = [];
                        if (activityDict.TryGetValue(today.Day, out dayActivities))
                        {
                            newDay.ActivityIcons = dayActivities;

                            var icons = dayActivities.GroupBy(icon => icon.Id)
                                                        .Select(iGrp => iGrp.First())
                                                        .ToList();
                        }
                        else
                        {
                            newDay.ActivityIcons = [];
                        }
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
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
