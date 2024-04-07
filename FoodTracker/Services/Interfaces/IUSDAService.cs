using FoodTracker.Models.USDA;

namespace FoodTrackerWeb.Services.Interfaces
{
    public interface IUSDAService
    {
        Task<USDABrandedQueryResult> Search(string userQuery);
        Task<USDABrandedQueryResult> Search(FoodSearchCriteria search);

    }
}
