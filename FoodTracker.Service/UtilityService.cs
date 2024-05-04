using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Reaction;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    public class UtilityService(string userId, IUnitOfWork unitOfWork) : IUtilityService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private string UserId { get; } = userId;
        public Dictionary<string, Icon> GetReactionIconDict()
        {
            return GetIconByType(IconType.Reaction);
        }
        private Dictionary<string, Icon> GetIconByType(IconType type)
        {
            return _unitOfWork.Icon.GetAll(i => i.Type == type)
                                    .ToDictionary(r => r.Name, r => r);
        }


        public IEnumerable<Color> GetAllColors()
        {
            return _unitOfWork.Color.GetAll();
        }


        public IEnumerable<Unit> GetAllVolumeUnits() 
        {
            return _unitOfWork.Unit.GetAll(u => u.UnitTypeId == 1); // TODO: conv mag num to enum
        }
        public IEnumerable<Unit> GetAllTimeUnits()
        {
            return _unitOfWork.Unit.GetAll(u => u.UnitTypeId == 2); // TODO: conv mag num to enum
        }



      
    }
}
