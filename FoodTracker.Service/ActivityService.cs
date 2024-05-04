using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
namespace FoodTracker.Service
{
    public class ActivityService(string userId, IUnitOfWork unitOfWork) : IActivityService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private string UserId { get; } = userId;
        public List<Icon> GetActiveIcons(DateTime date)
        {

            // search activity list
            // -- match on userId and datetime; include icon
            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == UserId && a.DateTime == date,
                                                    includeProperties: [Prop.ACTIVITY_ICON]);
            // create icon with tooltip

            if (activities == null)
            {
                return [];
            }    
            var activityIcons = new List<Icon>();
            foreach (var activity in activities)
            {
                var icon = new Icon 
                { 
                    Name = activity.ActivityType.Icon.Name,
                    HTML = activity.ActivityType.Icon.HTML
                };
                activityIcons.Add(icon);
            }
            // returl icon list

            return activityIcons;

        }

        public Dictionary<int, List<Icon>> GetMonthActivitiesDict(DateTime date) 
        {
            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == UserId &&
                                                    a.DateTime.Month == date.Month,
                                                    includeProperties: [Prop.ACTIVITY_ICON])
                                                    .GroupBy(a => a.DateTime.Day)
                                                    .ToDictionary(a => a.Key, a => 
                                                        a.Select(a => a.ActivityType.Icon)
                                                    .ToList());
            return activities;
        }
    }
}
