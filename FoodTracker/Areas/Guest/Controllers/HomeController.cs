using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    //[Authorize]
    public class HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IConfiguration config) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _config = config;

        public IActionResult Index()
        {            
            // Test Load
            var food = _unitOfWork.Food.GetAll(includeProperties: [Prop.FODMAP_CATEGORY, Prop.FODMAP_COLOR]); 
            return View(food);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
