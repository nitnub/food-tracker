using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public IActionResult Index()
        {
            //var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            //Console.WriteLine(new string('*', 40));
            //Console.WriteLine(env);
            //Console.WriteLine(Env.ASPNETCORE_ENVIRONMENT);
            //Console.WriteLine(_config["AllowedHosts"]);
            //Console.WriteLine(_config["ConnectionStrings:DefaultConnection"]);
            //var food = _unitOfWork.Food.GetAll(includeProperties: "Fodmap,Fodmap.Category,Fodmap.Color"); // Fodmap is not necessary when child items are included. What is best practice?
            
            var food = _unitOfWork.Food.GetAll(includeProperties: [Prop.FODMAP_CATEGORY, Prop.FODMAP_COLOR]); // Fodmap is not necessary when child items are included. What is best practice?

            //if (Env.SQL_SCRIPT_DIRECTORY != null)
            //{

            //    Console.WriteLine(Env.SQL_SCRIPT_DIRECTORY);
            //}
            //else
            //{
            //    Console.WriteLine("Is empty!");
            //}

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
