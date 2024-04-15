using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [ValidateNever]
        public Color Color { get; set; }
        [ValidateNever]
        public IEnumerable<MealType> MealTypes { get; set; }
        [ValidateNever]
        public IEnumerable<Food.Food> Foods { get; set; }
        [ValidateNever]
        public IEnumerable<Unit> Units { get; set; }
        public Dictionary<string, MealItem> MealItems { get; set; }
        //public IEnumerable<MealItem> MealItems { get; set; }
        //public List<int> Reactions { get; set; }
        public Dictionary<string, List<ReactionType>> Categories { get; set; }
        public Dictionary<int, bool> Reactions { get; set; }
    }
}
