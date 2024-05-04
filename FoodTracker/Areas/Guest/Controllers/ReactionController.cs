using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Service.IService;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class ReactionController(IUnitOfWork unitOfWork, IServiceManager serviceManager) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IReactionService _reactionService = serviceManager.Reaction;
        private readonly IFoodService _foodService = serviceManager.Food;
        public ReactionVM ReactionVM { get; set; }


        public IActionResult Index()
        {
            ReactionVM = new ReactionVM
            {
                Foods = _foodService.GetAll(),
            };

            return View(ReactionVM);
        }

        [HttpGet]
        public IActionResult GetReactions(int activeFoodId)
        {
            ReactionVM = new ReactionVM
            {
                Categories = _reactionService.GetReactionCategoryDict(),
                Severities = _reactionService.GetAllSeverities(),
                ActiveFood = _foodService.Get(activeFoodId),
                ExistingReactions = _reactionService.GetExistingReactionSeveritiesDict()
            };

            return PartialView("_ReactionPartial", ReactionVM);
        }

        [HttpPost]
        public IActionResult ToggleReaction([FromBody] Reaction reaction)
        {
            var updatedColor = "";

            if (ModelState.IsValid)
            {
                updatedColor = _reactionService.ToggleReaction(reaction);
            }

            return Json(new { updatedColor });

        }

        [HttpPost]
        public IActionResult UpdateUserSafeFood(int id)
        {
            var active = false;
            var success = false;
            var message = "Error updating user's safe foods";
            var userId = Helper.GetAppUserId(User);
            var updatedColor = "";

            Food food;

            if (userId == null)
                message = "Unable to find user";

            else if (id == 0)
                message = "Unable to find food";

            else
            {
                active = _reactionService.ToggleUserSafeFood(id);
                updatedColor = _reactionService.GetMaxSeverityColorString(id);
            }

            return Json(new { success, active, message, updatedColor });
        }


    }
}
