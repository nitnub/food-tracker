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
