﻿namespace FoodTracker.Models.ViewModels
{
    public class CalendarVM
    {
        public List<Meal.Meal> Meals { get; set; }
        public DayVM[,]? DayVMs { get; set; }
        public IEnumerable<CalendarDay> Days { get; set; }
        public FoodVM FoodVM { get; set; }
        public DateTime ViewDate { get; set; }
        public int ViewMonth {  get; set; }
        public int ViewYear { get; set; }
        public int ViewDay { get; set; }
        public string DateShortString { get; set; }
        public DayReactionVM DayReactionVM { get; set; }
        public Dictionary<string, Icon> ReactionIcons { get; set; }
    }
}
