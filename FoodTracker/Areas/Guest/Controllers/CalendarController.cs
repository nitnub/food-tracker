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

            CalendarVM.Line1 = $"This is {(Month)month} the {day}. It is {(Day)dt.DayOfWeek}.";
            return View(CalendarVM);
        }
    }
}
