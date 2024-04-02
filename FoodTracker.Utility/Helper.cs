using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using System;
using System.Collections;
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

            if (food.UserSafeFoods?.Count() > 0) return SD.COLOR_GREEN;

            var maxSeverity = food.Reactions?
                                        .Select(r => r.Severity.Value)
                                        .DefaultIfEmpty(-1)
                                        .Max() ?? -1;

            return maxSeverity switch
            {
                //-1 => "",
                <= 1 => "",
                <= 5 => SD.COLOR_YELLOW,
                _ => SD.COLOR_RED
            };
        }


        public static string GetMaxKnownProductFodColorString(ArrayList items)
        {
            var maxFodColor = SD.COLOR_BLUE;
            foreach (var item in items)
            {
                if (item.GetType() == typeof(FoodVM))
                {
                    var food = ((FoodVM)item).Food;

                    if (food.Fodmap == null) continue;

                    var color = food.Fodmap.Color.Name;


                    //maxFodColor =
                    //                    (productMaxReactionColor == SD.COLOR_RED || ingredientMaxReactionColor == SD.COLOR_RED)
                    //                    ? SD.COLOR_RED
                    //                    : (productMaxReactionColor == SD.COLOR_YELLOW || ingredientMaxReactionColor == SD.COLOR_YELLOW)
                    //                    ? SD.COLOR_YELLOW
                    //                    : "";
                    if (color ==  SD.COLOR_RED) 
                        return SD.COLOR_RED;
                  
                    if (color == SD.COLOR_YELLOW) 
                        maxFodColor = SD.COLOR_YELLOW;
                    
                }   
 
            }
            return maxFodColor;
        }
    }
}
