using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.Reaction;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public class ReactionController(IUnitOfWork unitOfWork) : Controller
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public ReactionVM ReactionVM { get; set; }

        public IActionResult Index()
        {
            ReactionVM = new ReactionVM
            {
                Foods = _unitOfWork.Food.GetAll(includeProperties: [Prop.REACTIONS_SEVERITY, Prop.FODMAP_COLOR, Prop.USER_SAFE_FOODS]),
            };
            return View(ReactionVM);
        }

        [HttpGet]
        public IActionResult GetReactions(int activeFoodId)
        {
            var userId = Helper.GetAppUserId(User);
            if (userId == null)
            {
                return PartialView("_ReactionPartial", ReactionVM);
            }

            var existingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == userId);
            var foodTypeSeverityDict = Helper.GetFoodTypeSeverityDict(existingReactions);

            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: Prop.CATEGORY);
            var reactionDict = Helper.GetReactionDict(reactions);
            

            ReactionVM = new ReactionVM
            {
                Categories = reactionDict,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                ActiveFood = _unitOfWork.Food.Get(f => f.Id == activeFoodId && (f.AppUserId == userId || f.Global), includeProperties: Prop.USER_SAFE_FOODS),
                ExistingReactions = foodTypeSeverityDict
            };
            return PartialView("_ReactionPartial", ReactionVM);
        }

        [HttpPost]
        public IActionResult ToggleReaction([FromBody] Reaction r)
        {
            var userId = Helper.GetAppUserId(User);
            var success = false;
            Food? food = null;

            r.SourceTypeId = ReactionSource.Food;

            if (ModelState.IsValid && userId != null)
            {
                r.AppUserId = userId;

                if (!QueueRemovalOfRelatedReactions(_unitOfWork, r))
                    _unitOfWork.Reaction.Add(r);
                _unitOfWork.Save();

                food = _unitOfWork.Food.Get(f => f.Id == r.FoodId && 
                            (f.AppUserId == userId || f.Global), 
                            includeProperties: Prop.REACTIONS_SEVERITY);

                success = food != null;
            }

            return Json(new { success, updatedColor = Helper.GetMaxSeverityColorString(food).ToLower() });
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
                var existingSafeFood = _unitOfWork.UserSafeFood.Get(f => f.AppUserId == userId && f.FoodId == id);

                if (existingSafeFood != null)
                    _unitOfWork.UserSafeFood.Remove(existingSafeFood);

                else
                {
                    var safeFood = new UserSafeFood()
                    {
                        AppUserId = userId,
                        FoodId = id
                    };
                    _unitOfWork.UserSafeFood.Add(safeFood);
                    active = true;
                }
                success = true;
                message = "Safe foods updated";
                _unitOfWork.Save();
            }

            food = _unitOfWork.Food.Get(f => f.Id == id && (f.AppUserId == userId || f.Global), includeProperties: Prop.REACTIONS_SEVERITY);

            updatedColor = Helper.GetMaxSeverityColorString(food).ToLower();
            
            return Json(new { success, active, message, updatedColor });
        }

        // returns true if reaction is now empty
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
    }
}
