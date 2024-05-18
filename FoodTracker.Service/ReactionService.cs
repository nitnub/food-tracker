using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Day;
using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    internal class ReactionService(string userId, IUnitOfWork unitOfWork) : IReactionService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private string UserId { get; } = userId;


        
        public IEnumerable<Reaction> GetAll()
        {
            return _unitOfWork.Reaction.GetAll(u => u.AppUserId == UserId);
        }

        public List<Reaction> GetAllDayReactions(DateTime dateTime)
        {
            return _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                                r.SourceTypeId == ReactionSource.Day &&
                                                r.IdentifiedOn == dateTime,
                                                includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY])
                                                .ToList();
        }

        public Dictionary<int, List<Reaction>> GetAllDayReactionsForSurroundingMonths(DateTime dateTime)
        {
            var dh = new DateHelper(dateTime);

            var padLast = dh.GetLastMonthPad();
            var padNext = dh.GetNextMonthPad();
           
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                                r.SourceTypeId == ReactionSource.Day &&
                                                r.IdentifiedOn >= padLast &&
                                                r.IdentifiedOn <= padNext,
                                                includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY])
                                                .GroupBy(r => r.IdentifiedOn.Value.DayOfYear - dh.FirstDayOfMonth.DayOfYear)
                                                .ToDictionary(r => r.Key, r => r.ToList());

            for (int i = - 7; i <= daysInMonth + 7; i++)
            {
                if (!reactions.ContainsKey(i))
                {
                    reactions[i] = [];
                }
            }
            return reactions;
        }


        public Dictionary<int, List<Reaction>> GetAllDayReactionsForTheMonth(DateTime dateTime)
        {

            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                                r.SourceTypeId == ReactionSource.Day &&
                                                r.IdentifiedOn.Value.Year == dateTime.Year &&
                                                r.IdentifiedOn.Value.Month == dateTime.Month,
                                                includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY])
                                                .GroupBy(r => r.IdentifiedOn.Value.Day)
                                                .ToDictionary(r => r.Key, r => r.ToList());

            for (int i = 1; i <= daysInMonth; i++)
            {
                if (!reactions.ContainsKey(i))
                {
                    reactions[i] = [];
                }
            }
            return reactions;
        }

        public IEnumerable<ReactionSeverity> GetAllSeverities()
        {
            return _unitOfWork.ReactionSeverity.GetAll();
        }

        public Dictionary<string, List<ReactionType>> GetReactionCategoryDict()
        {
            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            return Helper.GetReactionDict(reactions);
        }

        public Dictionary<string, List<Reaction>> GetDayReactionDict(string dateFormat = SD.DATE_FORMAT)
        {
            var dayReactions = _unitOfWork.Reaction.GetAll(r => 
                                                r.AppUserId == UserId && 
                                                r.SourceTypeId == ReactionSource.Day,
                                                includeProperties: [Prop.SEVERITY, Prop.TYPE_CATEGORY_ICON]);

            return dayReactions.GroupBy(r => r.IdentifiedOn.Value.ToString(dateFormat))
                                                .ToDictionary(r => r.Key, r => r.ToList());
        }

        public Dictionary<int, Dictionary<int, int>> GetExistingReactionSeveritiesDict()
        {
            return Helper.GetFoodTypeSeverityDict(GetAll());
        }

        public Dictionary<DateTime, Dictionary<int, int>> GetDayTypeSeverityDict(DateTime dateTime)
        {
            var existingReactions = GetAllDayReactions(dateTime);
            return Helper.GetDayTypeSeverityDict(existingReactions);
        }

        public Dictionary<string, bool> GetUserSafeDaysForSurroundingMonths(DateTime dateTime, string dateFormat = SD.DATE_FORMAT)
        {

            var dh = new DateHelper(dateTime);

            var padLast = dh.GetLastMonthPad();
            var padNext = dh.GetNextMonthPad();

            var userSafeDays = _unitOfWork.UserSafeDay.GetAll(d => d.AppUserId == UserId &&
                                                d.Date.DayOfYear >= padLast.DayOfYear &&
                                                d.Date.DayOfYear <= padNext.DayOfYear);

            return userSafeDays.GroupBy(d => d.Date.ToString(dateFormat))
                                                .ToDictionary(d => d.Key, d => true);
        }

        public Dictionary<string, bool> GetUserSafeDaysDict(DateTime dt, string dateFormat = SD.DATE_FORMAT)
        {
            var userSafeDays = _unitOfWork.UserSafeDay.GetAll(d => d.AppUserId == UserId &&
                                                d.Date.Year == dt.Year &&
                                                d.Date.Month == dt.Month);

            return userSafeDays.GroupBy(d => d.Date.ToString(dateFormat))
                                                .ToDictionary(d => d.Key, d => true);
        }

        public string ToggleReaction(Reaction reaction)
        {
            reaction.SourceTypeId = ReactionSource.Food;
            reaction.AppUserId = UserId;

            if (!QueueRemovalOfRelatedReactions(_unitOfWork, reaction))
                _unitOfWork.Reaction.Add(reaction);
            
            _unitOfWork.Save();

            var food = _unitOfWork.Food.Get(f => f.Id == reaction.FoodId &&
                                                (f.AppUserId == UserId || f.IsGlobal),
                                                includeProperties: Prop.REACTIONS_SEVERITY);

            return GetMaxSeverityColorString(food);
        }


        public bool ToggleDayReaction(Reaction reaction)
        {

            if (IsUserSafeDay((DateTime)reaction.IdentifiedOn))
                return false;

            reaction.AppUserId = UserId;
            reaction.SourceTypeId = ReactionSource.Day;



            var success = false;
            var reactionToRemove = _unitOfWork.Reaction.Get(r =>
                                            r.SourceTypeId == ReactionSource.Day &&
                                            r.IdentifiedOn.Value.Date == reaction.IdentifiedOn.Value.Date &&
                                            r.TypeId == reaction.TypeId &&
                                            r.AppUserId == reaction.AppUserId);

            if (reactionToRemove == null)
            {
                _unitOfWork.Reaction.Add(reaction);
            }

            else
            {
                _unitOfWork.Reaction.Remove(reactionToRemove);

                if (reaction.TypeId != reactionToRemove.TypeId ||
                    reaction.SeverityId != reactionToRemove.SeverityId)
                {
                    _unitOfWork.Reaction.Add(reaction);
                }
            }

            _unitOfWork.Save();

            return true;
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
                if (newReaction.TypeId == reactionToRemove.TypeId &&
                    newReaction.SeverityId == reactionToRemove.SeverityId)
                {
                    unitOfWork.Reaction.Remove(reactionToRemove);

                    success = true;
                }
                else
                {
                    unitOfWork.Reaction.Remove(reactionToRemove);

                    success = true;
                }
            } 

            return success;
        }

        public string GetMaxSeverityColorString(int foodId)
        {
            var food = _unitOfWork.Food.Get(f => f.Id == foodId && (f.AppUserId == UserId || f.IsGlobal), 
                                                includeProperties: Prop.REACTIONS_SEVERITY);

            return GetMaxSeverityColorString(food);
        }
        public string GetMaxSeverityColorString(Food food)
        {
            return Helper.GetMaxSeverityColorString(food).ToLower();
        }

        public bool ToggleUserSafeFood(int id)
        {
            var active = false;
            var existingSafeFood = _unitOfWork.UserSafeFood.Get(f => f.AppUserId == UserId && f.FoodId == id);

            if (existingSafeFood != null)
                _unitOfWork.UserSafeFood.Remove(existingSafeFood);

            else
            {
                var safeFood = new UserSafeFood()
                {
                    AppUserId = UserId,
                    FoodId = id
                };
                _unitOfWork.UserSafeFood.Add(safeFood);
                active = true;
            }

            _unitOfWork.Save();

            return active;
        }

        public bool ToggleUserSafeDay(DateTime date)
        {
            var active = false;
            var existingSafeDay = _unitOfWork.UserSafeDay.Get(d => d.AppUserId == userId && d.Date == DateOnly.FromDateTime(date));

            if (existingSafeDay != null)
                _unitOfWork.UserSafeDay.Remove(existingSafeDay);

            else
            {
                var safeDay = new UserSafeDay()
                {
                    AppUserId = UserId,
                    Date = DateOnly.FromDateTime(date),
                };
                _unitOfWork.UserSafeDay.Add(safeDay);
                active = true;
            }

            _unitOfWork.Save();

            return active;
        }


        public bool IsUserSafeDay(DateTime dateTime)
        {
            var userSafe = _unitOfWork.UserSafeDay.Get(d => d.AppUserId == UserId &&
                                            d.Date == DateOnly.FromDateTime(dateTime)) != null;
            return userSafe;
        }

        private static bool QueueRemovalOfRelatedReactions(IUnitOfWork unitOfWork, Reaction newReaction)
        {
            var success = false;
            var reactionToRemove = unitOfWork.Reaction.Get(r =>
                                            r.FoodId == newReaction.FoodId &&
                                            r.TypeId == newReaction.TypeId &&
                                            r.AppUserId == newReaction.AppUserId);

            if (reactionToRemove != null)
            {
                unitOfWork.Reaction.Remove(reactionToRemove);
                success = reactionToRemove.FoodId == newReaction.FoodId &&
                                            reactionToRemove.TypeId == newReaction.TypeId &&
                                            reactionToRemove.SeverityId == newReaction.SeverityId;
            }

            return success;
        }

        public List<Reaction> CreateMealReactionsList(List<int> reactionIds)
        {
            var mealReactions = new List<Reaction>();

            foreach (var id in reactionIds)
            {
                mealReactions.Add(
                    new Reaction
                    {
                        AppUserId = UserId,
                        SourceTypeId = ReactionSource.Meal,
                        TypeId = id
                    });
            }

            return mealReactions;
        }
        public List<Reaction> CreateMealReactionsList(Dictionary<int, bool> reactionDict)
        {
            var mealReactions = new List<Reaction>();

            foreach (var (reactionId, active) in reactionDict)
            {
                if (!active) continue;

                mealReactions.Add(
                    new Reaction
                    {
                        AppUserId = UserId,
                        SourceTypeId = ReactionSource.Meal,
                        TypeId = reactionId
                    });
            }

            return mealReactions;
        }
        public Dictionary<int, List<ReactionIcon>> GetActiveIconsForSurroundingMonths(DateTime dateTime)
        {
            var dh = new DateHelper(dateTime);

            var padLast = dh.GetLastMonthPad();
            var padNext = dh.GetNextMonthPad();
            var daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

            var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                            r.SourceTypeId == ReactionSource.Day &&
                                            r.IdentifiedOn >= padLast &&
                                            r.IdentifiedOn <= padNext &&
                                            r.Severity.Name != SD.REACTION_LABEL_NONE,
                                            includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY])
                                            .GroupBy(r => r.IdentifiedOn.Value.DayOfYear - dh.FirstDayOfMonth.DayOfYear)
                                            .ToDictionary(r => r.Key, r => r);



            var iconDict = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction)
                                            .ToDictionary(r => r.Name, r => r);

            var output = new Dictionary<int, List<ReactionIcon>>();
            foreach (var reactionList in reactions.Values)
            {
                var reactionIconDict = reactionList.GroupBy(r => r.Type.Category.Name)
                                            .ToDictionary(m => m.Key, m => CreateReactionIcon(m, iconDict));

                var activeIcons = reactionIconDict.Select(r => r.Value)
                                            .OrderBy(r => r.Name)
                                            .ToList();

                if (activeIcons.Count == 0)
                {
                    var noReactions = new ReactionIcon()
                    {
                        Name = SD.NEUTRAL,
                        Color = SD.COLOR_GRAY.ToLower(),
                        HTML = iconDict[SD.REACTION_LABEL_UNKNOWN].HTML
                    };

                    output[reactionList.Key] = [noReactions];
                }

                output[reactionList.Key] = activeIcons;
            }

            var userSafeDays = _unitOfWork.UserSafeDay.GetAll(s => s.AppUserId == UserId &&
                                                            s.Date.DayOfYear >= padLast.DayOfYear &&
                                                            s.Date.DayOfYear <= padNext.DayOfYear)
                                                            .Select(s => s.Date.DayOfYear - dh.FirstDayOfMonth.DayOfYear).ToList();

            foreach (int day in userSafeDays)
            {
                output[day] = [GetUserSafeDayIcon()];
            }
            for (int i = -7; i <= daysInMonth + 7; i++)
            {
                if (!output.ContainsKey(i))
                {
                    output[i] = [GetNeutralDayIcon()];
                }
            }

            return output;
        }

        public Dictionary<int, List<ReactionIcon>> GetActiveIcons(int year, int month)
        {

            var daysInMonth = DateTime.DaysInMonth(year, month);
      

            var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                            r.SourceTypeId == ReactionSource.Day &&
                                            r.IdentifiedOn.Value.Date.Year == year && 
                                            r.IdentifiedOn.Value.Date.Month == month &&
                                            r.Severity.Name != SD.REACTION_LABEL_NONE,
                                            includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY])
                                            .GroupBy(r => r.IdentifiedOn.Value.Day)
                                            .ToDictionary(r => r.Key, r => r);



            var iconDict = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction)
                                            .ToDictionary(r => r.Name, r => r);

            var output = new Dictionary<int, List<ReactionIcon>>();
            foreach (var reactionList in reactions.Values)
            {
                var reactionIconDict = reactionList.GroupBy(r => r.Type.Category.Name)
                                            .ToDictionary(m => m.Key, m => CreateReactionIcon(m, iconDict));

                var activeIcons = reactionIconDict.Select(r => r.Value)
                                            .OrderBy(r => r.Name)
                                            .ToList();

                if (activeIcons.Count == 0)
                {
                    var noReactions = new ReactionIcon()
                    {
                        Name = SD.NEUTRAL,
                        Color = SD.COLOR_GRAY.ToLower(),
                        HTML = iconDict[SD.REACTION_LABEL_UNKNOWN].HTML
                    };

                    output[reactionList.Key] = [noReactions];
                }

                output[reactionList.Key] = activeIcons;
            }

            var userSafeDays = _unitOfWork.UserSafeDay.GetAll(s => s.AppUserId == UserId &&
                                                                        s.Date.Year == year &&
                                                                        s.Date.Month == month)
                                                                        .Select(s => s.Date.Day).ToList();

            var userSafeIcon = GetUserSafeDayIcon();
            var neutralIcon = GetNeutralDayIcon();

            foreach (int day in userSafeDays)
            {
                    output[day] = [userSafeIcon];
            }

            for (int i = 1; i <=  daysInMonth; i++)
            {
                if (!output.ContainsKey(i))
                {
                    output[i] = [neutralIcon];
                }
            }

            return output;
        }
        public List<ReactionIcon> GetActiveIcons(DateTime date)
        {

            
            if (IsUserSafeDay(date))
            {
                return [GetUserSafeDayIcon()];
            }

            var reactions = _unitOfWork.Reaction.GetAll(r => r.AppUserId == UserId &&
                                        r.SourceTypeId == ReactionSource.Day &&
                                        r.IdentifiedOn.Value.Date == date.Date &&
                                        r.Severity.Name != SD.REACTION_LABEL_NONE,
                                        includeProperties: [Prop.TYPE_CATEGORY_ICON, Prop.SEVERITY]);

            var iconDict = _unitOfWork.Icon.GetAll(i => i.Type == IconType.Reaction)
                                        .ToDictionary(r => r.Name, r => r);

            var reactionIconDict = reactions.GroupBy(r => r.Type.Category.Name)
                                        .ToDictionary(m => m.Key, m => CreateReactionIcon(m, iconDict));

            var activeIcons = reactionIconDict.Select(r => r.Value)
                                        .OrderBy(r => r.Name)
                                        .ToList();

            if (activeIcons.Count == 0)
            {
                var noReactions = new ReactionIcon()
                {
                    Name = SD.NEUTRAL,
                    Color = SD.COLOR_GRAY.ToLower(),
                    HTML = iconDict[SD.REACTION_LABEL_UNKNOWN].HTML
                };

                return [noReactions];
            }
            return activeIcons;
        }
        public ReactionIcon GetUserSafeDayIcon()
        {
            return GetDayIcon(SD.REACTION_DAY_LABEL_NONE, SD.COLOR_GREEN);
        }

        public ReactionIcon GetNeutralDayIcon()
        {
            return GetDayIcon(SD.REACTION_LABEL_UNKNOWN, SD.COLOR_GRAY); 
        }

        private ReactionIcon GetDayIcon(string iconName, string color = SD.COLOR_GRAY)
        {
            var icon = _unitOfWork.Icon.Get(i => i.Name == iconName);

            return new()
            {
                Name = icon.Name,
                HTML = icon.HTML,
                Color = color.ToLower()
            };

        }

        private ReactionIcon CreateReactionIcon(IGrouping<string, Reaction> reaction, Dictionary<string, Icon> iconDict)
        {
            return new ReactionIcon()
            {
                Color = Helper.GetColorStringFromSeverity(reaction.ToList().Max(r => r.Severity.Value)).ToLower(),
                HTML = iconDict[reaction.Key].HTML,
                Name = iconDict[reaction.Key].Name
            };
        }

        string IReactionService.GetDayColorString(DateTime date)
        {
            var dayColor = SD.COLOR_BLUE;

            if (IsUserSafeDay(date))
            {
                return SD.COLOR_GREEN;
            }
            else
            {
                var reactions = GetAllDayReactions(date);

                if (reactions != null && reactions.Count > 0)
                    dayColor = Helper.GetReactiveColorString(reactions);

            }

            return dayColor;
        }
    }
}
