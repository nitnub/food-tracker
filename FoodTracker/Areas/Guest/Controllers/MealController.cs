using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
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
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        private readonly ICalendarService _calendarService = serviceManager.Calendar;
        private readonly IFoodService _foodService = serviceManager.Food;
        private readonly IMealService _mealService = serviceManager.Meal;
        private readonly IReactionService _reactionService = serviceManager.Reaction;
        private readonly IUtilityService _utilityService = serviceManager.Utility;
        private readonly IActivityService _activityService = serviceManager.Activity;
        public IActionResult Index(CalendarVM vm)
        {

        }

        #region API 
      

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
            //return PartialView("_AddMealPartial", GetMealVMFromDayVM(dayVM));
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
