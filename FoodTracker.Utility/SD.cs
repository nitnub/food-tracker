using FoodTracker.Models.Food;

namespace FoodTracker.Utility
{
    public static class SD
    {
        public const string DEVELOPMENT = "Development";

        public const string ROLE_ADMIN = "Admin";
        public const string ROLE_APP_USER = "AppUser";
        public const string ROLE_DELEGATE = "Delegate";

        public const string USDA_URL_SEARCH_POST = "https://api.nal.usda.gov/fdc/v1/foods/search?api_key=";
        public const string USDA_URL_SEARCH_GET = "https://api.nal.usda.gov/fdc/v1/foods/search";
        public const string USDA_URI_SEARCH_UPC_GET = "https://api.nal.usda.gov/fdc/v1/foods/search?query=";

        public const string COLOR_GREEN = "Green";
        public const string COLOR_YELLOW = "Yellow";
        public const string COLOR_RED = "Red";
        public const string COLOR_BLUE = "Blue";
        public const string COLOR_GRAY = "Gray";

        public const int UNIT_VOLUME = 1;
        public const int UNIT_TIME = 2;

        public const string NEUTRAL = "Neutral";
        public const string REACTION_SEVERITY_NONE = "None";
        public const string REACTION_LABEL_UNKNOWN = "No Reactions";
        public const string REACTION_DAY_LABEL_NONE = "Feeling Good!";

        public const string DATE_FORMAT = "yyyy-MM-dd";
    }
}