using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
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
        private readonly List<CalendarDay> _calendarDays = new();
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
        public IActionResult Index()
        {


            var mealDict = new Dictionary<string, List<Meal>>(); 

            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == Helper.GetAppUserId(User), includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME]);


            foreach (var meal in meals)
            {
                var tempList = new List<Meal>();
                var dateKey = meal.DateTime.ToString("yyyy-MM-dd").Substring(0, 10);
                if (!mealDict.TryGetValue(dateKey, out tempList))
                {
                    mealDict[dateKey] = new List<Meal>();
                    //mealDict.Add(dateKey, tempList);
                }

                mealDict[dateKey].Add(meal);
            }



            CalendarVM = new()
            {
                //Meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == Helper.GetAppUserId(User), includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME]).ToList()
            };


            DateTime dt = DateTime.Now;

            var month = dt.Month;
            var year = dt.Year;
            var day = dt.Day;
            var hour = dt.Hour;
            var minute = dt.Minute;
            var second = dt.Second;
            var millisecond = dt.Millisecond;
            // what day / position does the month start on
            // Index of first day of month

            var testMonth = month;
            var testYear = year;
            var firstDayOfMonth = new DateTime(testYear, testMonth, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            // how long is month
            var daysInMonth = DateTime.DaysInMonth(testYear, testMonth);

            // how many weeks to show
            var rowsNeeded = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;
            // rows x 7 days (0-6)

            CalendarVM.Daysb = new string[rowsNeeded][];
            //var daysj = new string[rowsNeeded,7];
            var daysj = new CalendarDay[rowsNeeded, 7];

            int dayIndex = 0 - firstDayOfMonthIndex;
            for (int row = 0; row < rowsNeeded; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    CalendarDay newDay = new();
                    Meal m1 = new() { Id = 1, DateTime = DateTime.Now };

                    MealItem mi1 = new() { Id = 1, MealId = 1, };


                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        //daysj[row, col] = $"xxxxxxxxxx";

                        newDay.Day = null;

                        daysj[row, col] = newDay;
                    }
                    else
                    {
                        // if exists in dict, add to day as dayVM
                        var dayKey = $"{testYear}-{testMonth}-{dayIndex:D2}";
                        var dayMeals = new List<Meal>();
                        if (mealDict.TryGetValue(dayKey, out dayMeals))
                        {
                            newDay.Meals = dayMeals;
                        }
                        else
                        {
                            newDay.Meals = new List<Meal>();

                        }
                        

                        //CalendarVM.Meals.
                        
                        newDay.Day = dayIndex + 1;
                        newDay.Color = new Color { Name = "Red" };

                        daysj[row, col] = newDay;


                        //daysj[row, col] = $"r-{row} c-{col} d-{dayIndex + 1}";

                    }

                    _calendarDays.Add(newDay);
                    dayIndex++;
                }
            }
            CalendarVM.DaysJ = daysj;



            // what is today
            CalendarVM.Line1 = $"This is {(Month)month} the {day}. It is {(Day)dt.DayOfWeek}.";
            CalendarVM.Line2 = daysInMonth.ToString();
            CalendarVM.Line3 = $"The total days in {(Month)month - 2} = {daysInMonth}";
            CalendarVM.Line4 = $"The first day of the month index is {firstDayOfMonthIndex}";

            CalendarVM.Line5 = $"Rows needed: {rowsNeeded}";
            return View(CalendarVM);

        }


        [HttpPost]
        public IActionResult Index(MealVM mealVM)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            Meal newMeal = mealVM.Meal;
            newMeal.AppUserId = Helper.GetAppUserId(User);

            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == newMeal.AppUserId || f.Global).Select(f => f.Id).ToList();

            newMeal.MealItems = [];
            foreach (var (key, value) in mealVM.MealItems)
            {
                if (!validUserFoods.Contains(value.FoodId))
                    continue;
                newMeal.MealItems.Add(value);
            }

            _unitOfWork.Meal.Add(newMeal);
            _unitOfWork.Save();

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult UpsertMeal(MealVM MealVM)
        {
            Meal meal = new() { DateTime = DateTime.Now };

            var id = 0;
            var foodUnsorted = _unitOfWork.Food.GetAll(f => f.AppUserId == Helper.GetAppUserId(User) || f.Global).OrderBy(x => x.Name);
            //foodUnsorted.ToList().OrderBy(x => x.Name);
            MealVM = new()
            {
                Meal = new() { DateTime = DateTime.Now },
                Foods = _unitOfWork.Food.GetAll(f => f.AppUserId == Helper.GetAppUserId(User) || f.Global).OrderBy(x => x.Name),
                MealTypes = _unitOfWork.MealType.GetAll(),
                Units = _unitOfWork.Unit.GetAll(u => u.Type == 1)
            };
            if (id == 0)
                return PartialView("_AddMealPartial", MealVM);

            return PartialView("_AddMealPartial", MealVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
