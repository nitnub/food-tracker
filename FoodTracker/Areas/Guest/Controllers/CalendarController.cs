using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    public class CalendarController : Controller
    {
        CalendarVM CalendarVM { get; set; }
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

            CalendarVM.Days = new string[rowsNeeded][];
            var daysj = new string[rowsNeeded,7];

            int dayIndex = 0 - firstDayOfMonthIndex;
            for (int row = 0; row < rowsNeeded; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        daysj[row, col] = $"xxxxxxxxxx";

                    } 
                    else
                    {
                        daysj[row, col] = $"r-{row} c-{col} d-{dayIndex + 1}";

                    }

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
    }
}
