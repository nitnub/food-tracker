using FoodTracker.Models;

namespace FoodTracker.Service.IService
{
    public interface IActivityService
    {
        List<Icon> GetActiveIcons(DateTime date);
        Dictionary<int, List<Icon>> GetMonthActivitiesDict(DateTime date);    
    }
}
