using FoodTracker.Models.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class CalendarVM
    {

        public string? Line1 { get; set; }
        public string? Line2 { get; set; }
        public string? Line3 { get; set; }
        public string? Line4 { get; set; }
        public string? Line5 { get; set; }


        public List<Meal.Meal> Meals { get; set; } 

        public string[][]? Daysb { get; set; }
        public CalendarDay[,]? DaysJ { get; set; }

        public IEnumerable<CalendarDay> Days { get; set; }

        public FoodVM FoodVM { get; set; }
    }
}
