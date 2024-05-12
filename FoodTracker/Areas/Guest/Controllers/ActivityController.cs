using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Activity;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{


    [Area("Guest")]
    [Authorize]
    public class ActivityController(IUnitOfWork unitOfwork) : Controller
    {
        public ActivityVM ActivityVM { get; set; }
        public ActivityGroupVM ActivityGroupVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetDayActivities(DateTime dateTime)
        {

            var userId = Helper.GetAppUserId(User);

            var activities = _unitOfWork.Activity.GetAll(a => a.AppUserId == userId &&
                                                    a.DateTime.Date == dateTime.Date,
                                                    includeProperties: [
                                                        Prop.ACTIVITY_INTENSITY,
                                                        Prop.ACTIVITY_TYPE])
                                                    .ToList();

            if (activities.Count < 0)
            {
                var initialActivity = new Activity()
                {
                    Id = 0,
                    DateTime = dateTime.AddHours(12),
                };
            }
            Dictionary<string, ActivityVM> activitiesDict = [];

            var initialActivityTest = new Activity()
            {
                Id = 0,
                DateTime = dateTime.AddHours(12),
            };

            ActivityVM = new()
            {
                Activity = initialActivityTest,
                Hours = 0,
                Minutes = 0,
            };

            var activityVMs = new List<ActivityVM>();

            foreach (var activity in activities)
            {
                var newVM = new ActivityVM()
                {
                    Activity = activity,
                    Hours = activity.Duration.Hours,
                    Minutes = activity.Duration.Minutes,
                };
                activitiesDict.Add(activity.Id.ToString(), newVM);
            }

            activityVMs.Add(ActivityVM);
            ActivityGroupVM = new()
            {
                Activities = activitiesDict,
                Intensities = _unitOfWork.ActivityIntensity.GetAll(),
                Types = _unitOfWork.ActivityType.GetAll(),
                DateTime = dateTime
            };

            return PartialView("_UpsertActivityPartial", ActivityGroupVM);
        }

        [HttpPost]
        public IActionResult UpsertDayActivities(ActivityGroupVM activityGroupVM)
        {
            CalendarVM = new()
            {
                ViewDate = activityGroupVM.DateTime.Date,
            };

            var userId = Helper.GetAppUserId(User);
            if (!ModelState.IsValid || userId == null)
                return RedirectToAction(nameof(Index), "Calendar", CalendarVM);

            var existingActivities = _unitOfWork.Activity.GetAll(a => a.AppUserId == userId &&
                                                                 a.DateTime.Date == activityGroupVM.DateTime.Date);

            if (existingActivities != null && existingActivities.Any())
            {
                _unitOfWork.Activity.RemoveRange(existingActivities);
                _unitOfWork.Save();
            }

            foreach (var vm in activityGroupVM.Activities.Values)
            {
                var activityDateTime = new DateTime(
                                                activityGroupVM.DateTime.Year,
                                                activityGroupVM.DateTime.Month,
                                                activityGroupVM.DateTime.Day,
                                                vm.Activity.DateTime.Hour,
                                                vm.Activity.DateTime.Minute,
                                                0);
                vm.Activity.AppUserId = userId;
                vm.Activity.DateTime = activityDateTime;
                vm.Activity.Duration = new TimeSpan(vm.Hours, vm.Minutes, 0);
                _unitOfWork.Activity.Add(vm.Activity);
            }

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index), "Calendar", CalendarVM);
        }

        [HttpDelete]
        public IActionResult DeleteActivity(int? id)
        {
            bool success = false;
            if (id != null && id != 0)
            {
                var userId = Helper.GetAppUserId(User);
                var activityToDelete = _unitOfWork.Activity.Get(a => a.Id == id && a.AppUserId == userId);

                if (activityToDelete != null)
                {
                    _unitOfWork.Activity.Remove(activityToDelete);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return Json(new { success });
        }
    }
}
