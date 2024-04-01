using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using FoodTracker.Models.ViewModels;
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

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            _unitOfWork.FodmapAlias.GetAll(); // load global FMAP aliases
            //_unitOfWork.FoodAlias.GetAll(fa => fa.AppUserId == userId); // load user's food aliases
            FoodVM = new()
            {
                FoodList = _unitOfWork.Food.GetAll(f => f.AppUserId == userId || f.Global == true, includeProperties: "UserSafeFoods,Fodmap.Color,Reactions"),
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
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;


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
            ////return null;
            return Json(new { originalName });
            //return Json(new { list = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits") });
            //return Json(new { list = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits") });
            return Redirect(HttpContext.Request.Headers["Referer"]);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetFoodDetailsByName(string foodName)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            // can be either identified or not

            // firt try to find by food name
            var matchedByFood = _unitOfWork.Food.Get(f => f.Name.ToLower() == foodName.ToLower() && (f.AppUserId == userId || f.Global));
            FoodAlias aliasFor;
            // if can't find by food name, try to find by alias


            if (matchedByFood == null)
            {
                aliasFor = _unitOfWork.FoodAlias.Get(a => a.Alias.ToLower() == foodName.ToLower() 
                                                            && (a.AppUserId == userId || a.Global)); // Note: Can't use .Equals + StringComparison w/DB call
                if (aliasFor == null)
                {

                    var newAlias = new FoodAlias() { Alias = foodName, FoodId = 0, AppUserId = userId, Global = false };

                    FoodVM = new FoodVM()
                    {
                        Food = new Food() { Name = foodName, Aliases = new List<FoodAlias>() { newAlias } },
                        FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits"),
                        
                    };

                    // if nothing matches, should be empty with id = 0
                    return PartialView("_AddFoodPartial", FoodVM);
                }
                else
                {
                    matchedByFood = _unitOfWork.Food.Get(f => f.Id == aliasFor.FoodId);
                }
            }

            _unitOfWork.FodmapAlias.GetAll(); // load aliases
            matchedByFood.Aliases = _unitOfWork.FoodAlias.GetAll(fa => fa.FoodId == matchedByFood.Id && (fa.AppUserId == userId || fa.Global)); // load user's food aliases
            
            
            FoodVM = new FoodVM()
            {
                Food = matchedByFood,
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits")
            };

            // if not, should be empty with id = 0

            return PartialView("_AddFoodPartial", FoodVM);




            var food = new Food();
            // if identified, prepopulate with info and food id
            if (aliasFor != null)
            {
                food = _unitOfWork.Food.Get(f => f.Id == aliasFor.FoodId);

            }
            FoodVM = new FoodVM()
            {
                Food = food,
                FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits")
            };

            // if not, should be empty with id = 0

            return PartialView("_AddFoodPartial", FoodVM);
        }

        [HttpGet]
        public IActionResult GetFoodDetailsById(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            // can be either identified or not
            //_unitOfWork.FoodAlias.GetAll(); // load aliases

            //_unitOfWork.Fodmap.GetAll();
            _unitOfWork.FodmapAlias.GetAll(); // load aliases
            var food = new Food();
            // if identified, prepopulate with info and food id
            if (id != 0)
            {
                //food = _unitOfWork.Food.Get(f => f.Id == id && (f.AppUserId == userId || f.Global == true), includeProperties: "Aliases");
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

            // if not, should be empty with id = 0

            return PartialView("_AddFoodPartial", FoodVM);
        }


        //[HttpGet]
        //public IActionResult GetFodmapAliases(int? fodId)
        //{
        //    bool success = false;
        //    IEnumerable<FodmapAlias> aliases = [];

        //    if (fodId != null)
        //    {
        //        aliases = _unitOfWork.FodmapAlias.GetAll(a => a.FodmapId == fodId);
        //        success = true;
        //    }

        //    return Json(new { success, aliases });        
        //}

        [HttpGet]
        public IActionResult GetFodmapList()
        {
            //bool success = false;
            //IEnumerable<FodmapAlias> aliases = [];

            //if (fodId != null)
            //{
            //    aliases = _unitOfWork.FodmapAlias.GetAll(a => a.FodmapId == fodId);
            //    success = true;
            //}

            _unitOfWork.FodmapAlias.GetAll();
            return Json(new { list = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits") } );
            //return Json(new { success, aliases });
        }


        //[HttpPost]
        //public IActionResult AddFoodAlias(int id, string newAlias)
        //{
        //    var success = false;
        //    var message = "Unexpected error adding Food Alias";
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;




        //    if (id == 0 || newAlias == "")
        //    {
        //        message = "Invalid Alias";
        //    }
        //    else
        //    {
        //        var existingFoodAlias = _unitOfWork.FoodAlias.Get(fa => fa.AppUserId == userId && 
        //                                                            fa.FoodId == id && 
        //                                                            fa.Alias.ToLower() == newAlias.ToLower());

        //        var aliasToAdd = new FoodAlias()
        //        {
        //            AppUserId = userId,
        //            FoodId = id,
        //            Alias = newAlias,
        //            Global = false
        //        };

        //        if ( existingFoodAlias != null)
        //        {
        //            aliasToAdd.Id = existingFoodAlias.Id;
        //            _unitOfWork.FoodAlias.Update(aliasToAdd);

        //        }
        //        else
        //        {
        //            _unitOfWork.FoodAlias.Add(aliasToAdd);

        //        }
        //        _unitOfWork.Save();
        //            success = true;
        //        message = "Alias updated successfully";

        //    }


        //    return Json(new { success, message});
        //}

        [HttpDelete]
        public IActionResult RemoveFoodAlias(int id, string alias)
        {

            var success = false;
            var message = $"Unexpected error removing Food Alias {alias}";
            //return Json(new { success, message = "test message " });
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

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


            //if (id != null)
            //{
            //    var aliasToRemove = _unitOfWork.FoodAlias.Get(a => a.AppUserId == userId && a.Id == id);

            //    _unitOfWork.FoodAlias.Remove(aliasToRemove);
            //    success = true;
            //}
            return Json(new { success, message });
        }
    }
}
