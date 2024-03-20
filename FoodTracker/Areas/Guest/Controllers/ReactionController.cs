using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.DataAccess.Repository;
using FoodTracker.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using FoodTracker.Models.Reaction;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Authorization;

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
                Severities = _unitOfWork.ReactionSeverity.GetAll()
            };
            return View(ReactionVM);
        }
    }
}
