using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Service.IService;
using System.Security.Claims;

namespace FoodTracker.Service
{
    public class ServiceManager : IServiceManager
    {
        private readonly ClaimsPrincipal _user;
        private readonly IUnitOfWork _unitOfWork;

        public IActivityService Activity { get; }
        public ICalendarService Calendar { get; }
        public IFoodService Food { get; }
        public IFodmapService Fodmap { get; }
        public IReactionService Reaction { get;  }
        public IMealService Meal { get; }
        public IUtilityService Utility { get; }


        public ServiceManager(ClaimsPrincipal user, IUnitOfWork unitOfWork)

        {
            _user = user;
            _unitOfWork = unitOfWork;

            var claimsIdentity = (ClaimsIdentity)_user.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            Activity = new ActivityService(userId, _unitOfWork);
            Calendar = new CalendarService(userId, _unitOfWork);
            Food = new FoodService(userId, _unitOfWork);
            Fodmap = new FodmapService(_unitOfWork);
            Reaction = new ReactionService(userId, _unitOfWork);
            Meal = new MealService(userId, _unitOfWork);
            Utility = new UtilityService(userId, _unitOfWork);
        }
    }
}
