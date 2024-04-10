using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Meal;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            CalendarVM = new();
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

            var testMonth = month -2;
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
            var daysj = new CalendarDay[rowsNeeded,7];

            int dayIndex = 0 - firstDayOfMonthIndex;
            for (int row = 0; row < rowsNeeded; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    CalendarDay newDay = new();
                    Meal m1 = new() { Id = 1, DateTime = DateTime.Now};

                    MealItem mi1 = new() { Id = 1, MealId = 1, };


                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        //daysj[row, col] = $"xxxxxxxxxx";

                        newDay.Day = null;

                        daysj[row, col] = newDay;
                    } 
                    else
                    {

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
            CalendarVM.Line3 = $"The total days in {(Month)month -2} = {daysInMonth}";
            CalendarVM.Line4 = $"The first day of the month index is {firstDayOfMonthIndex}";

            CalendarVM.Line5 = $"Rows needed: {rowsNeeded}";
            return View(CalendarVM);

        }

        [HttpGet]
        public IActionResult UpsertMeal(int id)
        {
            Meal meal = new() { DateTime = DateTime.Now};
            MealVM = new()
            {
                Meal = new() { DateTime = DateTime.Now },
            Units = _unitOfWork.Unit.GetAll()
            };
            if (id == 0)
                return PartialView("_AddMealPartial", MealVM);

            return PartialView("_AddMealPartial", MealVM);

            return RedirectToAction(nameof(Index));
        }
    }
}
