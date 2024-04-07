using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class FoodController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public FoodVM FoodVM { get; set; }

        public IActionResult Index()
        {

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = Helper.GetAppUserId(User);

            _unitOfWork.FodmapAlias.GetAll(); // load global FMAP aliases
            _unitOfWork.FoodAlias.GetAll(fa => fa.AppUserId == userId || fa.Global); // load user's food aliases
            FoodVM = new()
            {
                FoodList = _unitOfWork.Food.GetAll(f => f.AppUserId == userId || f.Global == true, includeProperties: "UserSafeFoods,Fodmap.Color,Reactions.Severity"),
                Food = new Food() { Id = 0, Aliases = [] },
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits"),
            };

            
            return View(FoodVM);
        }

        public IActionResult Delete(int? id)
        {
            var isSuccess = false;

            if (id == null || id == 0)
            {
                return Json(new { success = isSuccess });
            }

            var foodToRemove = _unitOfWork.Food.Get(f => f.Id == id);
            if (foodToRemove != null && !foodToRemove.Global)
            {
                _unitOfWork.Food.Remove(foodToRemove);
                _unitOfWork.Save();
                isSuccess = true;
            }
            
            return Json(new { success = isSuccess });

        }


        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            string originalName = food.Name;
            if (ModelState.IsValid)
            {
                //var claimsIdentity = (ClaimsIdentity)User.Identity;
                //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                var userId = Helper.GetAppUserId(User);

                var existingAliases = _unitOfWork.FoodAlias.GetAll(fa => fa.AppUserId == userId && fa.FoodId == food.Id);
                _unitOfWork.FoodAlias.RemoveRange(existingAliases);

                food.AppUserId = userId;

                if (food.Id == 0)
                {
                    _unitOfWork.Food.Add(food);
                }
                else
                {
                    var existingFood = _unitOfWork.Food.Get(f => f.Id == food.Id && f.AppUserId == userId);
                    if (existingFood != null && !existingFood.Global)
                    {
                        _unitOfWork.Food.Update(food);
                    }
                }

                if (food.Aliases != null)
                {
                    foreach (var el in food.Aliases)
                    {
                        el.Id = 0; // Mark as new with Id of 0
                        el.AppUserId = userId;
                        el.Global = false;     
                        _unitOfWork.FoodAlias.Add(el);
                    }
                }

                _unitOfWork.Save();
            }

            return Json(new { originalName });
        }

        [HttpGet]
        public IActionResult GetFoodDetailsByName(string foodName)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = Helper.GetAppUserId(User);


            var matchedByFood = _unitOfWork.Food.Get(f => f.Name.ToLower() == foodName.ToLower() && (f.AppUserId == userId || f.Global));
            FoodAlias aliasFor;

            _unitOfWork.FodmapAlias.GetAll(); // load global FODMAP aliases
            FoodVM = new FoodVM
            {
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits")
            };


            if (matchedByFood == null)
            {
                aliasFor = _unitOfWork.FoodAlias.Get(a => a.Alias.ToLower() == foodName.ToLower() 
                                                            && (a.AppUserId == userId || a.Global)); // Note: Can't use .Equals + StringComparison w/DB call
                if (aliasFor == null)
                {
                    var newAlias = new FoodAlias() { Alias = foodName, FoodId = 0, AppUserId = userId, Global = false };
                    FoodVM.Food = new Food() { Name = foodName, Aliases = new List<FoodAlias>() { newAlias } };

                    return PartialView("_AddFoodPartial", FoodVM);
                }
                else
                {
                    matchedByFood = _unitOfWork.Food.Get(f => f.Id == aliasFor.FoodId);
                }
            }

            matchedByFood.Aliases = _unitOfWork.FoodAlias.GetAll(fa => fa.FoodId == matchedByFood.Id && (fa.AppUserId == userId || fa.Global)); // load user's food aliases

            FoodVM.Food = matchedByFood;

            return PartialView("_AddFoodPartial", FoodVM);
        }

        [HttpGet]
        public IActionResult GetFoodDetailsById(int id)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = Helper.GetAppUserId(User);
            _unitOfWork.FodmapAlias.GetAll(); // load aliases
            var food = new Food();
            // if identified, prepopulate with info and food id
            if (id != 0)
            {
                food = _unitOfWork.Food.Get(f => f.Id == id && (f.AppUserId == userId || f.Global), includeProperties: "Aliases");
                food.Aliases = _unitOfWork.FoodAlias.GetAll(fa => fa.FoodId == id && (fa.AppUserId == userId || fa.Global)) ; // load user's food aliases
                if (food.Aliases == null)
                {
                    food.Aliases = new List<FoodAlias>();
                }
            }
            FoodVM = new FoodVM()
            {
                Food = food,
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits")
            };

            return PartialView("_AddFoodPartial", FoodVM);
        }

        [HttpGet]
        public IActionResult GetFodmapList()
        {
            _unitOfWork.FodmapAlias.GetAll();
            return Json(new { list = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits") } );
        }

        [HttpDelete]
        public IActionResult RemoveFoodAlias(int id, string alias)
        {

            var success = false;
            var message = $"Unexpected error removing Food Alias {alias}";

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userId = Helper.GetAppUserId(User);

            if (id == 0)
            {
                message = "Invalid Alias";
            }
            else
            {
                var existingFoodalias = _unitOfWork.FoodAlias.Get(fa => fa.Id == id && fa.AppUserId == userId);

                if (existingFoodalias != null && !existingFoodalias.Global)
                {
                    _unitOfWork.FoodAlias.Remove(existingFoodalias);
                    _unitOfWork.Save();
                }
                success = true;
                message = $"Food Alias {alias} has been removed";
            }

            return Json(new { success, message });
        }
    }
}
