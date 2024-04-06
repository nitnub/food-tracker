using FoodTracker.Models.Food;

namespace FoodTracker.Utility
{
    public class SD
    {
        public const string Development = "Development";

        public const string Role_Admin = "Admin";
        public const string Role_AppUser = "AppUser";
        public const string Role_Delegate = "Delegate";

        public const string USDA_URL_SEARCH_POST = "https://api.nal.usda.gov/fdc/v1/foods/search?api_key=";
        public const string USDA_URL_SEARCH_GET = "https://api.nal.usda.gov/fdc/v1/foods/search";
        public const string USDA_URI_SEARCH_UPC_GET = "https://api.nal.usda.gov/fdc/v1/foods/search?query=";
        //public const string USDA_URL_SEARCH_GET = "https://api.nal.usda.gov/fdc/v1/foods/search?query=cheddar%20cheese&dataType=Foundation,SR%20Legacy&pageSize=25&pageNumber=2&sortBy=dataType.keyword&sortOrder=asc&brandOwner=Kar%20Nut%20Products%20Company";

        public const string COLOR_GREEN = "Green";
        public const string COLOR_YELLOW = "Yellow";
        public const string COLOR_RED = "Red";
        public const string COLOR_BLUE = "Blue";



        // Food Properties
        public const string PROP_USER_SAFE_FOODS = "UserSafeFoods";
        public const string PROP_FODMAP_COLOR = "Fodmap.Color";
        public const string PROP_REACTIONS_SEVERITY = "Reactions.Severity";
        public const string PROP_CATEGORY = "Category";
        public const string PROP_COLOR = "Color";
        public const string PROP_MAX_USE_UNITS = "MaxUseUnits";
        public const string PROP_ALIASES = "Aliases";


        // Product Properties
        public const string PROP_FODMAP_ALIASES = "Fodmap.Aliases";
        public const string PROP_FODMAP_CATEGORY = "Fodmap.Category";
        public const string PROP_FODMAP_MAX_USE_UNITS = "Fodmap.MaxUseUnits";


        // Home Properties
        public const string PROP_FODMAP = "Fodmap";
        
        // Reaction Properties
         
     
   
   



        //        // Food Properties
        //"UserSafeFoods,Fodmap.Color,Reactions.Severity"
        //"Category,Color,MaxUseUnits"
        // "Category,Color,MaxUseUnits"
        // "Aliases"
        //"Category,Color,MaxUseUnits"
        //"Category,Color,MaxUseUnits"
        //// Product Properties
        //"Category,Color,MaxUseUnits"
        //"Fodmap.Color,Fodmap.Aliases,Fodmap.Category,Fodmap.MaxUseUnits,Reactions.Severity,UserSafeFoods"
        // "Category,Color,MaxUseUnits"

        //// Home Properties
        //"Fodmap,Fodmap.Category,Fodmap.Color"

        //// Reaction Properties
        // "Reactions.Severity,Fodmap.Color,UserSafeFoods"
        //"Category"
        //"UserSafeFoods"
        //"Reactions.Severity"
        //"Reactions.Severity"

    }
}