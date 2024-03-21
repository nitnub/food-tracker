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
       

        public ReactionVM ReactionVM{ get; set; }


        public IActionResult Index()
        {

            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: "Category");
            var reactionDict = new Dictionary<string, List<ReactionType>>();


            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var user = _unitOfWork.AppUser.Get(u => u.Id == userId, includeProperties: "Reactions");
            var existingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == userId);

            var foodTypeSeverityDict = new Dictionary<int, Dictionary<int, int>>();
            //var foodTypeSeverityDict = new Dictionary<int, (int, int)>();
            foreach (var reaction in existingReactions)
            {
                var typeSeverityDict = new Dictionary<int, int>();



                if (!foodTypeSeverityDict.TryGetValue(reaction.FoodId, out typeSeverityDict))
                {
                    typeSeverityDict = new Dictionary<int, int>();
                    foodTypeSeverityDict[reaction.FoodId] = [];
                }

                //if (!typeSeverityDict.TryGetValue(reaction.TypeId, out int severity)) 
                //{
                //    typeSeverityDict[reaction.TypeId] = severity;
                //}

                //foodTypeSeverityDict[reaction.FoodId][reaction.TypeId] = severity;
                foodTypeSeverityDict[reaction.FoodId][reaction.TypeId] = reaction.SeverityId;
            }


            foreach (var reaction in reactions)
            {
                //if (reactionDict.TryGetValue(reaction.Category.Name, out List<ReactionType> types))
                //{
                //    types.Add(reaction);
                //}
                //else
                //{

                //    reactionDict[reaction.Category.Name] = [reaction];
                //}
                //List<ReactionType> types;
                if (!reactionDict.TryGetValue(reaction.Category.Name, out List<ReactionType> _))
                {
                    reactionDict[reaction.Category.Name] = [];
                }
                reactionDict[reaction.Category.Name].Add(reaction);
            }

            ReactionVM = new ReactionVM
            {
                Foods = _unitOfWork.Food.GetAll(includeProperties: "Fodmap.Color"),
                Categories = reactionDict,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                //ExistingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == userId)
                ExistingReactions = foodTypeSeverityDict
            };
            return View(ReactionVM);
        }

        //[HttpPost]
        //public IActionResult Index(int id)
        //{

        //    return RedirectToAction("Index(1)");
        //}

        [HttpGet]
        public IActionResult GetReactions(string foodId)
        //public IActionResult GetReactions(ReactionVM Model)
        {
            //ReactionVM = new()
            //{
            //    FoodId = foodId
            //};


            var reactions = _unitOfWork.ReactionType.GetAll(includeProperties: "Category");
            var reactionDict = new Dictionary<string, List<ReactionType>>();


            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            //var user = _unitOfWork.AppUser.Get(u => u.Id == userId, includeProperties: "Reactions");
            var existingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == userId);

            var foodTypeSeverityDict = new Dictionary<int, Dictionary<int, int>>();
            //var foodTypeSeverityDict = new Dictionary<int, (int, int)>();
            foreach (var reaction in existingReactions)
            {
                var typeSeverityDict = new Dictionary<int, int>();



                if (!foodTypeSeverityDict.TryGetValue(reaction.FoodId, out typeSeverityDict))
                {
                    typeSeverityDict = new Dictionary<int, int>();
                    foodTypeSeverityDict[reaction.FoodId] = [];
                }

                //if (!typeSeverityDict.TryGetValue(reaction.TypeId, out int severity)) 
                //{
                //    typeSeverityDict[reaction.TypeId] = severity;
                //}

                //foodTypeSeverityDict[reaction.FoodId][reaction.TypeId] = severity;
                foodTypeSeverityDict[reaction.FoodId][reaction.TypeId] = reaction.SeverityId;
            }


            foreach (var reaction in reactions)
            {
                //if (reactionDict.TryGetValue(reaction.Category.Name, out List<ReactionType> types))
                //{
                //    types.Add(reaction);
                //}
                //else
                //{

                //    reactionDict[reaction.Category.Name] = [reaction];
                //}
                //List<ReactionType> types;
                if (!reactionDict.TryGetValue(reaction.Category.Name, out List<ReactionType> _))
                {
                    reactionDict[reaction.Category.Name] = [];
                }
                reactionDict[reaction.Category.Name].Add(reaction);
            }

            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            //return RedirectToAction("Index");
            ReactionVM = new ReactionVM
            {
                Foods = _unitOfWork.Food.GetAll(includeProperties: "Fodmap.Color"),
                Categories = reactionDict,
                Severities = _unitOfWork.ReactionSeverity.GetAll(),
                //ExistingReactions = _unitOfWork.Reaction.GetAll(u => u.AppUserId == userId)
                FoodId = Int32.Parse(foodId),
                ExistingReactions = foodTypeSeverityDict
            };
            return PartialView("_ReactionPartial", ReactionVM);
        }
    }
}
