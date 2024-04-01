using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Utility
{
    public class Helper
    {

        public static string GetMaxSeverityColorString(Food? food)
        {
            if (food == null || food.Reactions == null) return "";

            var maxSeverity = food.Reactions?
                                        .Select(r => r.Severity.Value)
                                        .DefaultIfEmpty(-1)
                                        .Max() ?? -1;

            return maxSeverity switch
            {
                -1 => "",
                0 => "Green",
                <= 5 => "Yellow",
                _ => "Red"
            };
        }
    }
}
