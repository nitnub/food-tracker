using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Service.IService;
using FoodTracker.Utility;

namespace FoodTracker.Service
{
    public class FoodService(string userId, IUnitOfWork unitOfWork) : IFoodService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private string UserId { get; } = userId;

        public IEnumerable<Food> GetAll()
        {
            _unitOfWork.FoodAlias.GetAll(fa => fa.AppUserId == UserId || fa.Global); // load user's food aliases

            var allUserFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == UserId || f.Global,
                                                    includeProperties: [Prop.FODMAP_COLOR, Prop.REACTIONS_SEVERITY, Prop.USER_SAFE_FOODS]);

            return allUserFoods;
        }

        public IEnumerable<Food> GetAllSorted()
        {
            return GetAll().ToList().OrderBy(x => x.Name);
        }

        public Food Get(int id)
        {
            var food = _unitOfWork.Food.Get(f => f.Id == id && (f.AppUserId == UserId || f.Global),
                            includeProperties: [Prop.ALIASES, Prop.USER_SAFE_FOODS]);

            food.Aliases = food.Aliases.Where(a => a.AppUserId == UserId || a.Global);

            return food;
        }

        public Food Get(string foodName)
        {
            var matchedByFood = _unitOfWork.Food.Get(f => f.Name.ToLower() == foodName.ToLower() && (f.AppUserId == UserId || f.Global),
                includeProperties: Prop.ALIASES);

            FoodAlias aliasFor;

            if (matchedByFood == null)
            {
                aliasFor = _unitOfWork.FoodAlias.Get(a => a.Alias.ToLower() == foodName.ToLower()
                                                            && (a.AppUserId == UserId || a.Global)); // Note: Can't use .Equals + StringComparison w/DB call
                if (aliasFor == null)
                {
                    var newAlias = new FoodAlias() { Alias = foodName, FoodId = 0, AppUserId = UserId, Global = false };

                    return new Food() { Name = foodName, Aliases = new List<FoodAlias>() { newAlias } };
                }
                else
                {
                    matchedByFood = _unitOfWork.Food.Get(f => f.Id == aliasFor.FoodId, includeProperties: Prop.ALIASES);
                }
            }

            matchedByFood.Aliases = matchedByFood.Aliases.Where(fa => fa.FoodId == matchedByFood.Id && (fa.AppUserId == UserId || fa.Global)); // load user's food aliases

            return matchedByFood;
        }
        public Food GetNewFood()
        {
            return new Food() { Id = 0, Aliases = [] };
        }
        public void Upsert(Food food)
        {
            food.AppUserId = UserId;

            if (food.Id == 0)
            {
                _unitOfWork.Food.Add(food);
            }
            else
            {
                var existingFood = _unitOfWork.Food.Get(f => f.Id == food.Id && f.AppUserId == UserId);
                if (existingFood != null && !existingFood.Global)
                {
                    var existingAliases = _unitOfWork.FoodAlias.GetAll(fa => fa.AppUserId == UserId && fa.FoodId == existingFood.Id);

                    _unitOfWork.FoodAlias.RemoveRange(existingAliases);
                    _unitOfWork.Food.Update(food);
                }
            }

            if (food.Aliases != null)
            {
                foreach (var el in food.Aliases)
                {
                    el.Id = 0; // Mark as new with Id of 0
                    el.AppUserId = UserId;
                    el.Global = false;
                    _unitOfWork.FoodAlias.Add(el);
                }
            }

            _unitOfWork.Save();
        }

        public bool Remove(int? id)
        {
            var success = false;
            if (id != null && id != 0)
            {
                var foodToRemove = _unitOfWork.Food.Get(f => f.Id == id);
                if (foodToRemove != null && !foodToRemove.Global)
                {
                    _unitOfWork.Food.Remove(foodToRemove);
                    _unitOfWork.Save();
                    success = true;
                }
            }
            return success;
        }
    }
}
