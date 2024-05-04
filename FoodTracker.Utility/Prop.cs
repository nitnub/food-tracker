using FoodTracker.Models.Food;
using System.Data;

namespace FoodTracker.Utility
{
    public static class Prop
    {
        // Food Properties
        public const string USER_SAFE_FOODS = "UserSafeFoods";
        public const string FODMAP_COLOR = "Fodmap.Color";
        public const string REACTIONS_SEVERITY = "Reactions.Severity";
        public const string REACTIONS_TYPE = "Reactions.Type";
        public const string CATEGORY = "Category";
        public const string COLOR = "Color";
        public const string MAX_USE_UNITS = "MaxUseUnits";
        public const string ALIASES = "Aliases";

        // Product Properties
        public const string FODMAP_ALIASES = "Fodmap.Aliases";
        public const string FODMAP_CATEGORY = "Fodmap.Category";
        public const string FODMAP_MAX_USE_UNITS = "Fodmap.MaxUseUnits";

        // Home Properties
        // Reaction Properties
        public const string FODMAP = "Fodmap";
        public const string SEVERITY = "Severity";
        public const string TYPE_CATEGORY_ICON = "Type.Category.Icon";

        // Meal Properties
        public const string MEAL_ITEMS = "MealItems";
        public const string MEAL_ITEMS_FOOD = "MealItems.Food";
        public const string MEAL_ITEMS_FOOD_FODMAP_COLOR = "MealItems.Food.Fodmap.Color";
        public const string MEAL_ITEMS_VOLUME = "MealItems.VolumeUnits";

        // Activity Properties
        public const string ACTIVITY_DURATION_UNITS = "DurationUnits";
        public const string ACTIVITY_INTENSITY = "Intensity";
        public const string ACTIVITY_TYPE = "ActivityType";
        public const string ACTIVITY_LOCATION = "Location";
        public const string ACTIVITY_ICON = "ActivityType.Icon";
    }
}