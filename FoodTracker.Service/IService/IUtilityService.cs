using FoodTracker.Models;
using FoodTracker.Models.Reaction;
using FoodTracker.Utility;

namespace FoodTracker.Service.IService
{
    public interface IUtilityService
    {
        Dictionary<string, Icon> GetReactionIconDict();
        IEnumerable<Color> GetAllColors();
        IEnumerable<Unit> GetAllVolumeUnits();
        IEnumerable<Unit> GetAllTimeUnits();
        
    }
}
