using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.FODMAP;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    public class FodmapService(IUnitOfWork unitOfWork) : IFodmapService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public IEnumerable<Fodmap> GetAll()
        {
            return _unitOfWork.Fodmap.GetAll(includeProperties: [Prop.CATEGORY, Prop.COLOR, Prop.MAX_USE_UNITS, Prop.ALIASES]);
        }
    }
}
