namespace FoodTracker.Models
{
    public class CalendarDay
    {
        public int? Day {  get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public Color Color { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Meal.Meal> Meals { get; set; }
        public IEnumerable<Reaction.Reaction> Reactions { get; set; }
        public IEnumerable<Activity.Activity> Activities { get; set; }
    }
}
