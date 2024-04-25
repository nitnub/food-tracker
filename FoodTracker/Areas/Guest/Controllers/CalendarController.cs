using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Day;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{

    [Area("Guest")]
    [Authorize]
    public class CalendarController(IUnitOfWork unitOfwork) : Controller
    {
        public MealVM MealVM { get; set; }
        public CalendarVM CalendarVM { get; set; }
        public DayReactionVM DayReactionVM { get; set; }
        private readonly IUnitOfWork _unitOfWork = unitOfwork;
       
        public IActionResult Index(CalendarVM vm)
        {
            var dateFormat = SD.DATE_FORMAT;
            var userId = Helper.GetAppUserId(User);
            
            var meals = _unitOfWork.Meal.GetAll(m => m.AppUserId == userId,
                            includeProperties: [Prop.MEAL_ITEMS_FOOD, Prop.MEAL_ITEMS_VOLUME, Prop.MEAL_ITEMS_FOOD_FODMAP_COLOR]);
            var mealDict = meals.GroupBy(m => m.DateTime.ToString(dateFormat))
                                .ToDictionary(m => m.Key, m => m.ToList());

            var dayReactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == userId && r.SourceTypeId == ReactionSource.Day,
                            includeProperties: [Prop.SEVERITY, Prop.TYPE_CATEGORY_ICON]);
            var reactionDict = dayReactions.GroupBy(r => r.IdentifiedOn.Value.ToString(dateFormat))
                                .ToDictionary(r => r.Key, r => r.ToList());

            DateTime dt = vm.ViewDate.Year > 1 ? vm.ViewDate : DateTime.Now;
            var userSafeDays = _unitOfWork.UserSafeDay.GetAll(d => d.AppUserId == userId &&
                                                                d.Date.Year == dt.Year &&
                                                                d.Date.Month == dt.Month); 
            
            var usdDict = userSafeDays.GroupBy(d => d.Date.ToString(dateFormat)).ToDictionary(d => d.Key, d => true); 
            var iconDict = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction).ToDictionary(r => r.Name, r => r);

            CalendarVM = new()
            {
                ViewYear = dt.Year,
                ViewMonth = dt.Month,
                ViewDay = dt.Day,
                ViewDate = dt,
                DayVMs = GetPopulatedCalendarDays(mealDict, reactionDict, iconDict, usdDict, dt),
                ReactionIcons = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction).ToDictionary(r => r.Name, r => r),
            }; 

            return View(CalendarVM);
        }

        #region API 
        [HttpPost]
        public IActionResult PriorYear(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear - 1, vm.ViewMonth);
        }

        [HttpPost]
        public IActionResult NextYear(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear + 1, vm.ViewMonth);
        }

        [HttpPost]
        public IActionResult PriorMonth(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth - 1);
        }

        [HttpPost]
        public IActionResult NextMonth(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth + 1);
        }

        [HttpPost]
        public IActionResult JumpToDate(CalendarVM vm)
        {
            return RediretToUpdatedCalendar(vm.ViewYear, vm.ViewMonth);
        }

        [HttpPost]
        public IActionResult Index(MealVM mealVM)
        {

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            //return RedirectToAction("Index");
            var updatedMeal = mealVM.Meal;
            var appUserId = Helper.GetAppUserId(User);

            // verify mealId is for user
            var verifiedMeal = _unitOfWork.Meal.Get(m => m.Id == updatedMeal.Id && m.AppUserId == appUserId, 
                                                    includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

            // get and then delete all mealItems with that meal id
            if (verifiedMeal != null && verifiedMeal.MealItems.Count > 0)
            {
                _unitOfWork.MealItem.RemoveRange(verifiedMeal.MealItems.ToList());
            }

            var validUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == appUserId || f.Global)
                                                    .Select(f => f.Id).ToList();

            var verifiedMealItemsList = mealVM.MealItems.Values
                                                    .Where(mi => validUserFoods.Contains(mi.FoodId))
                                                    .ToList();

            // get and then delete all mealReactions with that meal id
            if (verifiedMeal != null && verifiedMeal.Reactions.Count > 0)
            {
                _unitOfWork.Reaction.RemoveRange(verifiedMeal.Reactions.ToList());
            }

            var verifiedMealReactionsList = new List<Reaction>();

            foreach (var (reactionId, _) in mealVM.Reactions)
            {
                verifiedMealReactionsList.Add(
                    new Reaction
                    {
                        AppUserId = appUserId,
                        SourceTypeId = ReactionSource.Meal,
                        TypeId = reactionId
                    });
            }

            updatedMeal.AppUserId = appUserId;
            updatedMeal.MealItems = verifiedMealItemsList;
            updatedMeal.Reactions = verifiedMealReactionsList;

            if (updatedMeal.Id == 0)
            {
                _unitOfWork.Meal.Add(updatedMeal);
            }
            else
            {
                _unitOfWork.Meal.Update(updatedMeal);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpsertMeal([FromBody] DayVM dayVM)
        {

            DateTime mealTime = dayVM.DateTime.Date == DateTime.Now.Date
                                    ? mealTime = DateTime.Now
                                    : mealTime = dayVM.DateTime.Date.AddHours(12);

            Meal? activeMeal = null;

            var userId = Helper.GetAppUserId(User);

            var priorReactions = new Dictionary<int, bool>();
            if (dayVM.ActiveMealId != 0)
            {
                activeMeal = _unitOfWork.Meal.Get(m => m.AppUserId == userId && m.Id == dayVM.ActiveMealId, includeProperties: [Prop.MEAL_ITEMS, Prop.REACTIONS_TYPE]);

                foreach (var reaction in activeMeal.Reactions)
                {
                    priorReactions[reaction.Type.Id] = true;
                    reaction.AppUserId = null;
                }
                activeMeal.Reactions = null;
            }

            activeMeal ??= new Meal() { DateTime = mealTime, MealItems = [new MealItem()] };
 
            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);

            MealVM = new()
            {
                ColorOptions = _unitOfWork.Color.GetAll(),
                Reactions = priorReactions,
                Categories = Helper.GetReactionDict(reactions),
                Meal = activeMeal,
                Foods = _unitOfWork.Food.GetAll(f => f.AppUserId == userId || f.Global).OrderBy(x => x.Name),
                MealTypes = _unitOfWork.MealType.GetAll(),
                Units = _unitOfWork.Unit.GetAll(u => u.Type == 1)
            };

            return PartialView("_AddMealPartial", MealVM);
        }

        [HttpDelete]
        public IActionResult RemoveMeal(int id)
        {
            var mealToDelete = _unitOfWork.Meal.Get(m => m.Id == id && m.AppUserId == Helper.GetAppUserId(User));

            if (mealToDelete != null)
            {
                _unitOfWork.Meal.Remove(mealToDelete);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    
        [HttpGet]
        public IActionResult GetDayReactions(DateTime dateTime)
        {
            var appUserId = Helper.GetAppUserId(User);
            var todaysReactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == appUserId && 
                                                        r.SourceTypeId == ReactionSource.Day && 
                                                        r.IdentifiedOn == dateTime);

            var reactionTypes = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            var userSafe = _unitOfWork.UserSafeDay.Get(d => d.AppUserId == appUserId &&
                                                            d.Date == DateOnly.FromDateTime(dateTime)) != null;

            DayReactionVM = new()
            {
                DateTime = dateTime,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                Categories = Helper.GetReactionDict(reactionTypes),
                ExistingReactions = Helper.GetDayTypeSeverityDict(todaysReactions),
                UserSafe = userSafe
            };

            return PartialView("_AddDayReactionPartial", DayReactionVM);
        }


        [HttpPost]
        public IActionResult ToggleDayReaction([FromBody] Reaction reaction)
        {
            var userId = Helper.GetAppUserId(User);
            var success = false;
            var dayColor = "";
            var activeIcons = new List<ReactionIcon>();
            var isUserSafeDay = false;

            reaction.SourceTypeId = ReactionSource.Day;

            if (ModelState.IsValid && userId != null)
            {
                reaction.AppUserId = userId;

                if (!QueueRemovalOfRelatedDayReactions(_unitOfWork, reaction))
                    _unitOfWork.Reaction.Add(reaction);
                
                _unitOfWork.Save();

                success = true;
        
                var userSafeDay = _unitOfWork.UserSafeDay.Get(d => d.AppUserId == userId && 
                                                            d.Date == DateOnly.FromDateTime((DateTime)reaction.IdentifiedOn));

                if (userSafeDay != null)
                {
                    isUserSafeDay = true;
                }
                else
                {
                    var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == userId &&
                                                                r.SourceTypeId == ReactionSource.Day &&
                                                                r.IdentifiedOn.Value.Date == reaction.IdentifiedOn.Value.Date,
                                                                includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY]);

                    var iconDict = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction).ToDictionary(r => r.Name, r => r);
                    activeIcons = GetActiveIcons(reactions, iconDict);

                    dayColor = Helper.GetDayColor(reactions, activeIcons);
                }
            }
            return Json(new { success, isUserSafeDay, activeIcons, dayColor });
        }

        [HttpPost]
        public IActionResult UpdateUserSafeDay(DateOnly date)
        {
            var active = false;
            var success = false;
            var message = "Error updating user's safe days";
            var userId = Helper.GetAppUserId(User);
            var updatedColor = "";
            var reactionIcons = new List<ReactionIcon>();

            if (userId == null)
                message = "Unable to find user";

            else if (date == DateOnly.MinValue)
                message = "Unable to find date";

            else
            {
                var existingSafeDay = _unitOfWork.UserSafeDay.Get(d => d.AppUserId == userId && d.Date == date);

                if (existingSafeDay != null)
                {
                    _unitOfWork.UserSafeDay.Remove(existingSafeDay);

                    var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == userId && 
                                                            r.SourceTypeId == ReactionSource.Day && 
                                                            DateOnly.FromDateTime(r.IdentifiedOn.Value.Date) == date,
                                                            includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY]);

                    var iconDict =  _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction).ToDictionary(r => r.Name, r => r);
                    reactionIcons = GetActiveIcons(reactions, iconDict);
                    
                    var drVM = new DayReactionVM() { Reactions = reactions };
                    updatedColor = Helper.GetMaxSeverityColorString(drVM).ToLower();
                }
                else
                {
                    var safeDay = new UserSafeDay()
                    {
                        AppUserId = userId,
                        Date = date,
                    };
                    _unitOfWork.UserSafeDay.Add(safeDay);
                    active = true;

                    updatedColor = SD.COLOR_GREEN;

                     var icon = _unitOfWork.Icon.Get(i => i.Name == SD.REACTION_LABEL_NONE);

                    ReactionIcon noReactions = new() 
                    {
                        Name = icon.Name, 
                        HTML = icon.HTML, 
                        Color = SD.COLOR_GREEN.ToLower() 
                    };

                    reactionIcons.Add(new() { Name = icon.Name, HTML = icon.HTML, Color = SD.COLOR_GREEN.ToLower() });
                }
                success = true;
                message = "Safe days updated";
                _unitOfWork.Save();

            }

            return Json(new { success, active, message, updatedColor, reactionIcons });
        }

        #endregion

    
        private static DayVM[,] GetPopulatedCalendarDays(Dictionary<string, List<Meal>> mealDict, Dictionary<string, List<Reaction>> reactionDict, Dictionary<string, Icon> iconDict, Dictionary<string, bool> userSafeDays, DateTime dt)
        {

            var dateFormat = SD.DATE_FORMAT;
            var firstDayOfMonth = new DateTime(dt.Year, dt.Month, 1);
            var firstDayOfMonthIndex = (int)firstDayOfMonth.DayOfWeek;

            var daysInMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            var weeksInMonth = (firstDayOfMonthIndex + daysInMonth) / 7 + 1;

            var dayVMs = new DayVM[weeksInMonth, 7];
            int dayIndex = 0 - firstDayOfMonthIndex;

            for (int row = 0; row < weeksInMonth; row++)
            {
                for (int col = 0; col < 7; col++)
                {
                    DayVM newDay = new();

                    // If day falls outside of current month...
                    if (dayIndex < 0 || dayIndex > daysInMonth - 1)
                    {
                        newDay.Day = null;
                    }
                    else
                    {
                        var today = new DateTime(dt.Year, dt.Month, dayIndex + 1);
                        var dayKey = today.ToString(dateFormat);

                        if (mealDict.TryGetValue(dayKey, out List<Meal>? dayMeals))
                        {
                            newDay.Meals = dayMeals;
                        }
                        else
                        {
                            newDay.Meals = [];
                        }

                        if (reactionDict.TryGetValue(dayKey, out List<Reaction>? dayReactions))
                        {
                            newDay.Reactions = dayReactions;
                            newDay.ReactionIcons = GetActiveIcons(dayReactions, iconDict);
                        }
                        else
                        {
                            newDay.Reactions = [];
                            newDay.ReactionIcons = [];
                        }

                        if (userSafeDays.ContainsKey(dayKey))
                        {
                            var noReactions = new ReactionIcon()
                            {
                                Name = SD.REACTION_LABEL_NONE,
                                Color = SD.COLOR_GREEN,
                                HTML = iconDict[SD.REACTION_LABEL_NONE].HTML
                            };


                            newDay.ReactionIcons.Add(noReactions);
                            newDay.UserSafeDay = true;
                        }

                        newDay.DateTime = today;
                        newDay.Day = dayIndex + 1;

                        if (newDay.UserSafeDay)
                        {
                            newDay.Color = SD.COLOR_GREEN.ToLower();
                        }
                        else if (newDay.Reactions.Count > 0)
                        {
                            newDay.Color = Helper.GetColorStringFromSeverity(newDay.Reactions.Select(r => r.Severity.Value).Max()).ToLower();

                        }
                        else
                        {
                            newDay.Color = "blue";
                        }
                    }
                    dayVMs[row, col] = newDay;
                    dayIndex++;
                }
            }

            return dayVMs;
        }

 

        private static List<ReactionIcon> GetActiveIcons(IEnumerable<Reaction>? dayReactions, Dictionary<string, Icon> iconDict)
        {

            var reactionIconDict = dayReactions.GroupBy(r => r.Type.Category.Name)
                                        .ToDictionary(m => m.Key, m =>
                                            new ReactionIcon()
                                            {
                                                Color = Helper.GetColorStringFromSeverity(m.ToList().Max(r => r.Severity.Value)).ToLower(),
                                                HTML = iconDict[m.Key].HTML,
                                                Name = iconDict[m.Key].Name
                                            });

            var activeIcons = reactionIconDict.Select(r => r.Value)
                                        .OrderBy(r => r.Name)
                                        .ToList();

            if (activeIcons.Count == 0) 
            {
                var noReactions = new ReactionIcon()
                {
                    Name = "Neutral",
                    Color = SD.COLOR_GRAY,
                    HTML = iconDict[SD.REACTION_LABEL_NONE].HTML
                };

                return [noReactions];
            }
            return activeIcons;
        }

        private RedirectToActionResult RediretToUpdatedCalendar(int year, int month = 1, int day = 1)
        {
            try
            {
                var newDate = new DateTime(year, month, day);
                var calendarVM = new CalendarVM
                {
                    ViewDate = newDate
                };

                return RedirectToAction(nameof(Index), calendarVM);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }

        private static bool QueueRemovalOfRelatedDayReactions(IUnitOfWork unitOfWork, Reaction newReaction)
        {
            var success = false;
            var reactionToRemove = unitOfWork.Reaction.Get(r =>
                                            r.SourceTypeId == ReactionSource.Day &&
                                            r.IdentifiedOn.Value.Date == newReaction.IdentifiedOn.Value.Date &&
                                            r.TypeId == newReaction.TypeId &&
                                            r.AppUserId == newReaction.AppUserId);

            if (reactionToRemove != null)
            {
                unitOfWork.Reaction.Remove(reactionToRemove);
                //success = reactionToRemove.SourceTypeId == ReactionSource.Day &&
                //                            reactionToRemove.IdentifiedOn.Value.Date == newReaction.IdentifiedOn.Value.Date &&
                //                            reactionToRemove.TypeId == newReaction.TypeId &&
                //                            reactionToRemove.SeverityId == newReaction.SeverityId;
                success = true;
            }

            return success;
        }
    }
}
