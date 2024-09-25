using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Meal;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class MealController(IUnitOfWork unitOfwork, IServiceManager serviceManager) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        public DayReactionVM DayReactionVM { get; set; }
        public string? UserTimeZone { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        private readonly ICalendarService _calendarService = serviceManager.Calendar;
        private readonly IFoodService _foodService = serviceManager.Food;
        private readonly IMealService _mealService = serviceManager.Meal;
        private readonly IReactionService _reactionService = serviceManager.Reaction;
        private readonly IUtilityService _utilityService = serviceManager.Utility;
        private readonly IActivityService _activityService = serviceManager.Activity;
            

        [HttpPost]
        public IActionResult UpsertMealTemplate([FromBody] MealVM mealVM)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false });
            }


            var tempDateTime = mealVM.Meal.DateTime;
            mealVM.Reactions ??= [];
            
            mealVM.Meal.DateTime = DateTime.MinValue;
            mealVM.Meal.IsTemplate = true;

            var mealItems = mealVM.MealItems.Values.ToList();
            var reactionIds = mealVM.Reactions.Keys.ToList();

            _mealService.Upsert(mealVM.Meal, mealItems, reactionIds);

            DayVM dayVM = new()
            {
                DateTime = tempDateTime,
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
        }

        [HttpPost]
        public IActionResult Submit(MealVM mealVM)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            mealVM.Reactions ??= [];
            mealVM.Meal.DateTime = mealVM.Meal.DateTime.Date + mealVM.Time;

            var mealItems = mealVM.MealItems.Values.ToList();
            var reactionIds = mealVM.Reactions.Keys.ToList();

            if (mealVM.Meal.IsTemplate)
            {
                _mealService.UpsertMealFromTemplate(mealVM.Meal, mealItems, reactionIds);
            }
            else
            {
                _mealService.Upsert(mealVM.Meal, mealItems, reactionIds);

            }
            CalendarVM = new() { ViewDate = mealVM.Meal.DateTime };

            return RedirectToAction("Index", "Calendar", new { area = "Guest", model = CalendarVM });
        }

        [HttpPost]
        public IActionResult UpsertMeal([FromBody] DayVM dayVM)
        {
            
            UserTimeZone = dayVM.UserTimeZone;

            var mealVM = GetMealVMFromDayVM(dayVM);
            if (mealVM.Meal.IsTemplate)
            {
                mealVM.Meal.Id = 0;
                foreach (var mealItem in mealVM.Meal.MealItems)
                {
                    mealItem.MealId = 0;
                    mealItem.Id = 0;
                }
            }

            return PartialView("_AddMealPartial", mealVM);
        }

        [HttpGet]
        public IActionResult GetTemplateMeal(int id, DateTime dateTime, TimeSpan mealTime)
        {
            DayVM dayVM = new()
            {
                DateTime = dateTime,
                ActiveMealId = id
            };

            return PartialView("_AddMealPartial", GetMealTemplateVMFromDayVM(dayVM));
        }

        [HttpDelete]
        public IActionResult RemoveMeal(int id)
        {
            var success = _mealService.Remove(id);


            return Json(new {success });
            
        }

        private MealVM GetMealTemplateVMFromDayVM(DayVM dayVM)
        {
            return GetMealVMFromDayVM(dayVM, true);
        }
        private MealVM GetMealVMFromDayVM(DayVM dayVM, bool asTemplate = false)
        {
            UserTimeZone ??= SD.DEFAULT_TIME_ZONE;
            var usersClickedDay = dayVM.DateTime;

            DateTime utc = DateTime.UtcNow;
            TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(UserTimeZone);
            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utc, zone);

            DateTime mealTime = usersClickedDay.Date == localDateTime.Date
              ? localDateTime
              : usersClickedDay.Date.AddHours(12);


            Meal? activeMeal = null;
            var priorReactions = new Dictionary<int, bool>();

            if (dayVM.ActiveMealId != 0)
            {
                activeMeal = _mealService.GetMealDetails(dayVM.ActiveMealId);
                priorReactions = _mealService.GetMealReactionDict(activeMeal);
                activeMeal.Reactions = null;
            }

            if (activeMeal == null)
            {
                activeMeal = _mealService.CreateBlankMeal(mealTime);
                activeMeal.DateTime = mealTime;
            }

            if (!asTemplate && activeMeal.IsTemplate)
            {
                activeMeal.Id = 0;
                activeMeal.IsTemplate = false;
            }

            if (asTemplate)
            {

                if (dayVM.DateTime.Date ==  localDateTime.Date)
                {
                    activeMeal.DateTime = DateTime.Now;
                } 
                else
                {
                    activeMeal.DateTime = dayVM.DateTime.AddHours(12);
                }
            }


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
