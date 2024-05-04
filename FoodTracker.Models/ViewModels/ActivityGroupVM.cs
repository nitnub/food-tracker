using FoodTracker.Models.Activity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FoodTracker.Models.ViewModels
{
    public class ActivityGroupVM
    {
        public Dictionary<string, ActivityVM> Activities {  get; set; }
        //public List<ActivityVM> Activities { get; set; }
        [ValidateNever]
        public IEnumerable<ActivityIntensity> Intensities { get; set; }
        [ValidateNever]
        public IEnumerable<ActivityType> Types { get; set; }

        public DateTime DateTime { get; set; }
    }
}
