using FoodTracker.Models.Meal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class MealVM
    {

        public Meal.Meal Meal {  get; set; }

        public IEnumerable<Unit> Units { get; set; }
        //public IEnumerable<MealItem> MealItems { get; set; }
    }
}
