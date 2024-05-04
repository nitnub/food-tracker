using FoodTracker.Models.Activity;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class ActivityVM
    {
        public Activity.Activity Activity { get; set; }
        //public IEnumerable<Unit> DurationUnits { get; set; }
        //public IEnumerable<ActivityIntensity> Intensities { get; set; }
        //public IEnumerable<ActivityType> Types { get; set; }

        //public TimeSpan Duration { get; set; }

        public int Hours { get; set; }
        public int Minutes { get; set; }
    }
}
