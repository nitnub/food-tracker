using Azure;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.NewFolder;
using FoodTracker.Models.USDA;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using FoodTrackerWeb.Services;
using FoodTrackerWeb.Services.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Collections;
using System.Drawing.Text;
using System.Net.Http;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    public partial class ProductController(IUnitOfWork unitOfWork, IUSDAService usdaService) : Controller
    {
        private IUnitOfWork _unitOfWork = unitOfWork;
        private USDABrandedQuery BrandedQuery;
        private ProductVM ProductVM;
        private IUSDAService _usdaService = usdaService;

        public IActionResult Index()
        {
            ProductVM = new();
            _unitOfWork.FodmapAlias.GetAll(); // load global FMAP aliases
            ProductVM.FoodVM = new FoodVM() { FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: "Category,Color,MaxUseUnits") };
            return View(ProductVM);
        }

        [HttpPost]
        public IActionResult Index(Food food)
        {
            ProductVM = new();
            return RedirectToAction("Food", "Index", food);
        }

        public async Task<IActionResult> GetUSDAProducts(string userQuery)
        {
            try
            {
                string[] ingredientSkipChars = [",", ".", ""];
                var productResponse = await _usdaService.Search(userQuery);

                if (!productResponse.Success)
                {
                    return PartialView("_ProductBrandedPartial", new ProductVM() { QueryError = true });
                }

                var knownFoodsDict = new Dictionary<string, Food>();
                var elementsDict = new Dictionary<int, ArrayList>();

                var currentTrackedFoods = _unitOfWork.Food.GetAll(includeProperties: "Fodmap.Color,Fodmap.Aliases,Fodmap.Category,Fodmap.MaxUseUnits,Reactions.Severity");

                foreach (var trackedFood in currentTrackedFoods)
                    knownFoodsDict[trackedFood.Name.ToLower()] = trackedFood;

                foreach (var food in productResponse.Foods)
                {
                    var elements = new ArrayList();

                    string[] ingredientEntries = IngredientDelimiters().Split(food.Ingredients);
                    foreach (string item in ingredientEntries)
                    {
                        if (ingredientSkipChars.Contains(item)) continue;
                        //if (item.Equals(",") || item.Equals(".") || item.Length == 0) continue;

                        if (item.Length == 1)
                        {
                            elements.Add(item);
                            continue;
                        }

                        //var existingFood = new Food();
                        //if (!knownFoodsDict.TryGetValue(item.Trim().ToLower(), out existingFood))
                        //    existingFood = new Food() { Name = item.Trim() };



                        var existingFood = new Food();
                        if (!knownFoodsDict.TryGetValue(item.Trim().ToLower(), out existingFood))
                            existingFood = new Food() { Name = item.Trim() };

                        var maxSeverity = existingFood?.Reactions?.Select(r => r.Severity.Value).DefaultIfEmpty(-1).Max() ?? -1;

                        var foodModel = new FoodVM();

                        //var color = maxSeverity == 0 ? "green" : maxSeverity <= 5 ? "yellow" : "red";
                        //var colors = "black";
                        //Console.WriteLine(maxSeverity);
                        //var color = maxSeverity switch
                        //{
                        //    -1   => "",
                        //    0    => "Green",
                        //    <= 5 => "Yellow",
                        //    _    => "Red"
                        //};
                        
                        foodModel.Food = existingFood;
                        foodModel.MaxReaction = (double)maxSeverity;
                        foodModel.MaxReactionColor = Helper.GetMaxSeverityColorString(existingFood);


                        //elements.Add(existingFood);
                        elements.Add(foodModel);
                    }

                    elementsDict[food.FdcId] = elements;
                }

                ProductVM = new()
                {
                    BrandedResult = productResponse,
                    IngredientsDict = elementsDict
                };

                return PartialView("_ProductBrandedPartial", ProductVM);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return PartialView("_ProductBrandedPartial", new ProductVM() { QueryError = true });
            }
        }



        [GeneratedRegex(@"([,(){}\[\]])")] // Performance recommendation from VS IDE
        private static partial Regex IngredientDelimiters();
    }


}
