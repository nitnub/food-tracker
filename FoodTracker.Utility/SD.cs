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
    }
}