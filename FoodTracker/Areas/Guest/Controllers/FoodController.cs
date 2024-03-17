using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    public class FoodController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public FoodVM FoodVM { get; set; }

        public IActionResult Index()
        {
            FoodVM = new()
            {
                FoodList = _unitOfWork.Food.GetAll(),
                Food = new Food() { Id = 0 },
                FodmapList = _unitOfWork.Fodmap.GetAll().Select(f => new SelectListItem()
                {
                    Text = f.Name,
                    Value = f.Id.ToString()
                }),
            };

            return View(FoodVM);
        }

        public IActionResult Delete(int? id)
        {

            if (id != null && id != 0)
            {
                var foodToRemove = _unitOfWork.Food.Get(f => f.Id == id);
                if (foodToRemove != null)
                {
                    _unitOfWork.Food.Remove(foodToRemove);
                    _unitOfWork.Save();
                }
            }


            //TempData["success"] = "Food deleted successfully";
            return Json(new { success = true, message = "Delete successful" });
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index(FoodVM foodVM)
        {
            var food = foodVM.Food;
            if (ModelState.IsValid)
            {
                if (food.Id == 0)
                {
                    _unitOfWork.Food.Add(food);

                }
                else
                {
                    _unitOfWork.Food.Update(food);
                }
            }
            _unitOfWork.Save();

            foodVM.FoodList = _unitOfWork.Food.GetAll().OrderBy(f => f.Name);
            foodVM.Food = new Food() { Id = 0 }; //, null; // foodVM.FoodList.Where(f => f.Name == food.Name).First();
            foodVM.FodmapList = _unitOfWork.Fodmap.GetAll().Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.Id.ToString()
            });
            return RedirectToAction("Index");
            //return View(foodVM);
        }

        //[HttpPost]
        //public IActionResult Upsert(Food food)
        //{
        //    //var food = foodVM.Food;
        //    if (ModelState.IsValid)
        //    {
        //        if (food.Id == 0)
        //        {
        //            _unitOfWork.Food.Add(food);

        //        }
        //        else
        //        {
        //            _unitOfWork.Food.Update(food);
        //        }
        //    }
        //    _unitOfWork.Save();

        //    //foodVM.FoodList = _unitOfWork.Food.GetAll().OrderBy(f => f.Name);
        //    //foodVM.Food = new Food() { Id = 0 }; //, null; // foodVM.FoodList.Where(f => f.Name == food.Name).First();
        //    //foodVM.FodmapList = _unitOfWork.Fodmap.GetAll().Select(f => new SelectListItem()
        //    //{
        //    //    Text = f.Name,
        //    //    Value = f.Id.ToString()
        //    //});
        //    return RedirectToAction("Index");
        //    //return View(foodVM);
        //}
    }
}
