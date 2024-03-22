using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.DataAccess.Repository;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FoodTracker.Models.Reaction;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;
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
                Foods = _unitOfWork.Food.GetAll(includeProperties: "Fodmap.Color"),
            };
            return View(ReactionVM);
        }


        [HttpGet]
        public IActionResult GetReactions(string activeFoodId)
        {
            var existingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == GetAppUserId(User));
            var foodTypeSeverityDict = new Dictionary<int, Dictionary<int, int>>();

            foreach (var reaction in existingReactions)
            {
                var typeSeverityDict = new Dictionary<int, int>();


                var foodId = reaction.FoodId;
                if (foodTypeSeverityDict.TryGetValue(foodId, out typeSeverityDict))
                {
                    typeSeverityDict[reaction.TypeId] = reaction.SeverityId;
                }
                else
                {
                    foodTypeSeverityDict[foodId] = [];
                    foodTypeSeverityDict[foodId][reaction.TypeId] = reaction.SeverityId;
                }

            }


            var reactionDict = new Dictionary<string, List<ReactionType>>();
            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: "Category");

            foreach (var reaction in reactions)
            {
                var category = reaction.Category.Name;
                
                if (reactionDict.TryGetValue(category, out List<ReactionType> categoryDict))
                {
                    categoryDict.Add(reaction);
                }
                else
                {
                    reactionDict[category] = [];
                    reactionDict[category].Add(reaction);
                }
            }

            ReactionVM = new ReactionVM
            {
                Categories = reactionDict,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                ActiveFood = _unitOfWork.Food.Get(f => f.Id == Int32.Parse(activeFoodId)),
                ExistingReactions = foodTypeSeverityDict
            };
            return PartialView("_ReactionPartial", ReactionVM);
        }


        [HttpPost]
        public IActionResult RemoveReaction([FromBody] Reaction reactionToRemove)
        {
            if (ModelState.IsValid)
            {
                reactionToRemove.AppUserId = GetAppUserId(User);
                QueueRemovalOfRelatedReactions(_unitOfWork, reactionToRemove);

                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddReaction([FromBody] Reaction newReaction)
        {

            if (ModelState.IsValid)
            {
                newReaction.AppUserId = GetAppUserId(User);
                QueueRemovalOfRelatedReactions(_unitOfWork, newReaction);

                _unitOfWork.Reaction.Add(newReaction);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        private static void QueueRemovalOfRelatedReactions(IUnitOfWork unitOfWork, Reaction newReaction) 
        {
            var reactionsToRemove = unitOfWork.Reaction.GetAll(r => r.FoodId == newReaction.FoodId
                                           && r.TypeId == newReaction.TypeId);

            if (reactionsToRemove != null)
            {
                unitOfWork.Reaction.RemoveRange(reactionsToRemove);
            }
        }

        private static string GetAppUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
