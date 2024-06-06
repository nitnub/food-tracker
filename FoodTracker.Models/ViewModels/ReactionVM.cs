using FoodTracker.Models.Reaction;

namespace FoodTracker.Models.ViewModels
{
    public class ReactionVM
    {
        public IEnumerable<Food.Food> Foods { get; set; }
        public Dictionary<int, Dictionary<int, int>> ExistingReactions { get; set; }
        public Dictionary<string, List<ReactionType>> Categories { get; set; }

        public IEnumerable<ReactionSeverity> Severities {  get; set; }

        public int FoodId {  get; set; }
        public Food.Food ActiveFood { get; set; }
        public FoodVM? FoodVM { get; set; }
    }
}
