using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class DayVM
    {
        public int? Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [ValidateNever]
        public string Color { get; set; }
        public int ActiveMealId { get; set; }
        public DateTime DateTime { get; set; }
        public List<Meal.Meal> Meals { get; set; }
        public List<Reaction.Reaction> Reactions { get; set; }

        public List<Activity.Activity> Activities { get; set; }
        public List<Icon> ActivityIcons { get; set; }

        public List<ReactionIcon> ReactionIcons { get; set; }
        public bool UserSafeDay { get; set; }
    }
}
