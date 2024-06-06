using FoodTracker.DataAccess.Interfaces;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace FoodTracker.Models.ViewModels
{
    public class DayReactionVM: IReactable
    {
        public int? Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        [ValidateNever]
        public Color Color { get; set; }
        public int ActiveMealId { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<Reaction.Reaction> Reactions { get; set; }
        public Dictionary<DateTime, Dictionary<int, int>> ExistingReactions { get; set; }
        public Dictionary<string, List<ReactionType>> Categories { get; set; }

        public IEnumerable<ReactionSeverity> Severities { get; set; }
        public List<Activity.Activity> Activities { get; set; }
        public Dictionary<string, Icon> ReactionIcons { get; set; }
        public bool IsUserSafe { get; set; }

    }
}
