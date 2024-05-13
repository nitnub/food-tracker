using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class FoodController(IUnitOfWork unitOfWork, IServiceManager serviceManager) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IServiceManager _serviceManager = serviceManager;
        private readonly IFoodService _foodService = serviceManager.Food;
        private readonly IFodmapService _fodmapService = serviceManager.Fodmap;
        public FoodVM FoodVM { get; set; }

        public IActionResult Index()
        {
            FoodVM = new()
            {
                FoodList = _foodService.GetAll(),
                Food = _foodService.GetNewFood(),
                FodmapList = _fodmapService.GetAll()
            };

            return View(FoodVM);
        }

        public IActionResult Delete(int? id)
        {
            var success = _foodService.Remove(id);

            return Json(new { success });

        }

        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            if (ModelState.IsValid)
            {
                _foodService.Upsert(food);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetFoodDetailsByName(string foodName)
        {
            FoodVM = new FoodVM
            {
                Food = _foodService.Get(foodName),
                FodmapList = _fodmapService.GetAll()
            };

            return PartialView("_AddFoodPartial", FoodVM);
        }

        [HttpGet]
        public IActionResult GetFoodDetailsById(int id)
        {

            var food = new Food();
            
            if (id != 0)
            {
                food = _foodService.Get(id);
            }
            FoodVM = new FoodVM()
            {
                Food = food,
                FodmapList = _fodmapService.GetAll()
            };

            return PartialView("_AddFoodPartial", FoodVM);
        }
    }
}
