using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Utility
{
    public class Helper
    {

        public static string GetMaxSeverityColorString(Food? food)
        {
            if (food == null || food.Reactions == null)
                return "";

            if (food.UserSafeFoods?.Count() > 0)
                return SD.COLOR_GREEN;

            var maxSeverity = food.Reactions?
                                        .Select(r => r.Severity.Value)
                                        .DefaultIfEmpty(-1)
                                        .Max() ?? -1;

            return maxSeverity switch
            {
                <= 1 => "",
                <= 5 => SD.COLOR_YELLOW,
                _ => SD.COLOR_RED
            };
        }


        public static string GetMaxKnownProductFodColorString(ArrayList items)
        {
            var maxFodColor = SD.COLOR_BLUE;
            Food food;
            string color;

            foreach (var item in items)
            {
                if (item.GetType() != typeof(FoodVM))
                    continue;

                food = ((FoodVM)item).Food;

                if (food.Fodmap == null)
                    continue;

                color = food.Fodmap.Color.Name;

                if (color == SD.COLOR_RED)
                    return SD.COLOR_RED;

                if (color == SD.COLOR_YELLOW)
                    maxFodColor = SD.COLOR_YELLOW;
            }
            return maxFodColor;
        }



        public static string? GetAppUserId(ClaimsPrincipal User)
        {
            ClaimsIdentity claimsIdentity;
            Claim? nameIdentifier;

            if (User.Identity == null)
                return null;

            claimsIdentity = (ClaimsIdentity)User.Identity;
            nameIdentifier = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (nameIdentifier == null)
                return null;

            return nameIdentifier.Value;
        }
    }
}
