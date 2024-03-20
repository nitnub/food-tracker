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
        public IEnumerable<Reaction.Reaction> Reactions { get; set; }
        public Dictionary<string, List<ReactionType>> Categories { get; set; }

        public IEnumerable<ReactionSeverity> Severities {  get; set; }
    }
}
