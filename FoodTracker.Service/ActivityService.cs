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
            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == UserId && a.DateTime == date,
                                            includeProperties: [Prop.ACTIVITY_ICON]);

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

            return activityIcons;
        }

        public Dictionary<int, List<Icon>> GetMonthActivitiesDict(DateTime date) 
        {
            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == UserId &&
                                            a.DateTime.Year == date.Year &&
                                            a.DateTime.Month == date.Month,
                                            includeProperties: [Prop.ACTIVITY_ICON])
                                            .GroupBy(a => a.DateTime.Day)
                                            .ToDictionary(a => a.Key, a => 
                                                a.Select(a => a.ActivityType.Icon)
                                            .ToList());
            return activities;
        }

        Dictionary<int, List<Icon>> IActivityService.GetMonthActivitiesForSurroundingMonths(DateTime dateTime)
        {
            var dh = new DateHelper(dateTime);
            var padLast = dh.GetLastMonthPad();
            var padNext = dh.GetNextMonthPad();

            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == UserId &&
                                            a.DateTime >= padLast &&
                                            a.DateTime <= padNext,
                                            includeProperties: [Prop.ACTIVITY_ICON])
                                            .GroupBy(a => a.DateTime.DayOfYear - dh.FirstDayOfMonth.DayOfYear)
                                            .ToDictionary(a => a.Key, a => a.Select(a => a.ActivityType.Icon)
                                            .ToList());
            return activities;
        }
    }
}
