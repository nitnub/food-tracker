using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class ReactionVM
    {
        public IEnumerable<Food.Food> Foods { get; set; }
        //public IEnumerable<Reaction.Reaction> ExistingReactions { get; set; 
        public Dictionary<int, Dictionary<int, int>> ExistingReactions { get; set; }
        public Dictionary<string, List<ReactionType>> Categories { get; set; }

        public IEnumerable<ReactionSeverity> Severities {  get; set; }

        public int FoodId {  get; set; }
        public Food.Food ActiveFood { get; set; }
    }
}
