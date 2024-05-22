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
        public IActionResult UpsertMealTemplate([FromBody] MealVM mealVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }

            //var updatedDateTime = mealVM.Meal.DateTime.Date + mealVM.Time;
            //mealVM.Meal.DateTime = updatedDateTime;
            mealVM.Reactions ??= [];

            var mealTemplate = mealVM.Meal;
            mealTemplate.DateTime = DateTime.MinValue;
            mealTemplate.IsTemplate = true;
            var mealItems = mealVM.MealItems.Values.ToList();
            var reactionIds = mealVM.Reactions.Keys.ToList();

           
            _mealService.Upsert(mealTemplate, mealItems, reactionIds);

            DayVM dayVM = new()
            {
                DateTime = mealVM.Meal.DateTime,
                ActiveMealId = _mealService.GetMatchingMealTemplateId(mealVM.Meal)
            };

            return PartialView("_AddMealPartial", GetMealVMFromDayVM(dayVM));
        }

        [HttpDelete]
        public IActionResult RemoveMealTemplate(int id, DateTime calendarDate)
        {

            _mealService.Remove(id);

            MealVM = new()
            {
                ColorOptions = _utilityService.GetAllColors(),
                Reactions = [],
                Categories = _reactionService.GetReactionCategoryDict(),
                Meal = _mealService.CreateBlankMeal(calendarDate),
                MealTemplates = _mealService.GetMealTemplateOptions(),
                Foods = _foodService.GetAllSorted(),
                MealTypes = _mealService.GetAllMealTypes(),
                Units = _utilityService.GetAllVolumeUnits(),
                CalendarDate = calendarDate
            };

            return PartialView("_AddMealPartial", MealVM);

            //return RedirectToAction(nameof(Index));



            //if (!ModelState.IsValid)
            //{
            //    return Json(new { success = false });
            //}

            //// find meal

            //// remove
            //_mealService.Remove();


            //mealVM.Reactions ??= [];

            //var mealTemplate = mealVM.Meal;
            //mealTemplate.DateTime = DateTime.MinValue;
            //mealTemplate.IsTemplate = true;
            //var mealItems = mealVM.MealItems.Values.ToList();
            //var reactionIds = mealVM.Reactions.Keys.ToList();

            //_mealService.Upsert(mealTemplate, mealItems, reactionIds);

            //DayVM dayVM = new()
            //{
            //    DateTime = mealVM.Meal.DateTime,
            //    ActiveMealId = _mealService.GetMatchingMealTemplateId(mealVM.Meal)
            //};

            //return PartialView("_AddMealPartial", GetMealVMFromDayVM(dayVM));
        }

        [HttpPost]
        public IActionResult Index(MealVM mealVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var updatedDateTime = mealVM.Meal.DateTime.Date + mealVM.Time;
                                //.AddHours(mealVM.Time.Hour)
                                //.AddMinutes(mealVM.Time.Minute);

            mealVM.Reactions ??= [];
            mealVM.Meal.DateTime = updatedDateTime;
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
            return PartialView("_AddMealPartial", GetMealVMFromDayVM(dayVM));
        }

        [HttpGet]
        public IActionResult GetTemplateMeal(int id, DateTime dateTime, TimeSpan mealTime)
        {
            var meal = _mealService.GetMealDetails(id);
            //meal.DateTime = dateTime;

            //if (meal.DateTime.Date == DateTime.Today)
            //{
            //    meal.DateTime = DateTime.Now;
            //}
            //else
            //{
            //    meal.DateTime = dateTime.AddHours(12);
            //}

            //

            //DateTime dt;
            //if (meal.DateTime.Date == DateTime.Today)
            //{
            //    dt = DateTime.Now;
            //}
            //else
            //{
            //    dt = dateTime.AddHours(12);
            //}


            DayVM dayVM = new()
            {
                DateTime = dateTime,
                ActiveMealId = id
            };

            return PartialView("_AddMealPartial", GetMealVMFromDayVM(dayVM));

            // 
            MealVM = new()
            {
                ColorOptions = _utilityService.GetAllColors(),
                Reactions = _mealService.GetMealReactionDict(meal),
                Categories = _reactionService.GetReactionCategoryDict(),
                Meal = meal,
                MealTemplates = _mealService.GetMealTemplateOptions(),
                Foods = _foodService.GetAllSorted(),
                MealTypes = _mealService.GetAllMealTypes(),
                Units = _utilityService.GetAllVolumeUnits(),
                //Time = dateTime.TimeOfDay
                Time = meal.DateTime.TimeOfDay,
                TemplateId = id
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
                IsUserSafe = _reactionService.IsUserSafeDay(dateTime)
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
            var dh = new DateHelper(dt);

            var dayVMs = new DayVM[dh.WeeksInMonth, 7];
            int dayIndex = dh.DayIndex;

            var mealIcons = _mealService.GetMealsForSurroundingMonths(dt);
            var activityDict = _activityService.GetMonthActivitiesForSurroundingMonths(dt);
            var reactionIcons = _reactionService.GetActiveIconsForSurroundingMonths(dt);

            var dayReactions = _reactionService.GetAllDayReactionsForSurroundingMonths(dt);
            var dayColors = _calendarService.GetDayColorsForSurroundingMonths(dt, dayReactions);

            for (int row = 0; row < dh.WeeksInMonth; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DayVM newDay;
                    var today = dh.GetTodayFromDayIndex(dayIndex);

                    if (dayIndex < 0 || dayIndex >= dh.DaysInMonth)
                    {
                        // If day falls outside of current month...
                    }

                    newDay = new()
                    {
                        Meals = mealIcons[dayIndex],
                        Reactions = dayReactions[dayIndex],
                        ReactionIcons = reactionIcons[dayIndex],
                        DateTime = today,
                        Day = dh.GetDayLabel(dayIndex),
                        Color = dayColors[dayIndex]
                    };

                    List<Icon> dayActivities = [];

                    if (activityDict.TryGetValue(dayIndex, out dayActivities))
                    {
                        newDay.ActivityIcons = dayActivities;
                    }
                    else
                    {
                        newDay.ActivityIcons = [];
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

        private MealVM GetMealVMFromDayVM(DayVM dayVM)
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
                MealTemplates = _mealService.GetMealTemplateOptions(),
                Foods = _foodService.GetAllSorted(),
                MealTypes = _mealService.GetAllMealTypes(),
                Units = _utilityService.GetAllVolumeUnits(),
                CalendarDate = dayVM.DateTime
            };
            return MealVM;
        }
    }
}
