using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.USDA;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using FoodTrackerWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Text.RegularExpressions;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
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
            ProductVM.FoodVM = new FoodVM() { FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: [Prop.CATEGORY, Prop.COLOR, Prop.MAX_USE_UNITS]) };

            return View(ProductVM);
        }

        [HttpPost]
        public IActionResult Index(Food food)
        {
            ProductVM = new();
            return RedirectToAction("Food", "Index", food);
        }

        public async Task<IActionResult> GetUSDAProducts(string userQuery, int pageNumber = 1)
        {
            try
            {
                _unitOfWork.FodmapAlias.GetAll(); // load global FMAP aliases

                string[] ingredientSkipChars = [",", ".", ""];
                var productResponse = await _usdaService.Search(new FoodSearchCriteria() { Query = userQuery, PageNumber = pageNumber });

                if (!productResponse.Success)
                {
                    return PartialView("_ProductBrandedPartial", new ProductVM() { QueryError = true });
                }

                var knownFoodsDict = new Dictionary<string, Food>();
                var elementsDict = new Dictionary<int, ArrayList>();

                var currentTrackedFoods = _unitOfWork.Food.GetAll(includeProperties: 
                        [Prop.FODMAP_COLOR, Prop.FODMAP_ALIASES, Prop.FODMAP_CATEGORY,Prop.FODMAP_MAX_USE_UNITS, Prop.REACTIONS_SEVERITY, Prop.USER_SAFE_FOODS]);

                foreach (var trackedFood in currentTrackedFoods)
                    knownFoodsDict[trackedFood.Name.ToLower()] = trackedFood;

                foreach (var food in productResponse.Foods)
                {
                    var productMaxReactionColor = "";
                    var elements = new ArrayList();
                    var ingredientEntries = IngredientDelimiters().Split(food.Ingredients);
                    
                    foreach (string item in ingredientEntries)
                    {
                        if (ingredientSkipChars.Contains(item)) 
                            continue;

                        if (item.Length == 1)
                        {
                            elements.Add(item);
                            continue;
                        }

                        var existingFood = new Food();
                        if (!knownFoodsDict.TryGetValue(item.Trim().ToLower(), out existingFood))
                            existingFood = new Food() { Name = item.Trim(), };

                        var ingredientMaxReactionColor = Helper.GetMaxSeverityColorString(existingFood);

                        productMaxReactionColor = 
                                        (productMaxReactionColor == SD.COLOR_RED || ingredientMaxReactionColor == SD.COLOR_RED) 
                                        ? SD.COLOR_RED 
                                        : (productMaxReactionColor == SD.COLOR_YELLOW || ingredientMaxReactionColor == SD.COLOR_YELLOW)
                                        ? SD.COLOR_YELLOW 
                                        : "";

                        var foodModel = new FoodVM
                        {
                            Food = existingFood,
                            MaxReactionColor = Helper.GetMaxSeverityColorString(existingFood),
                        };

                        elements.Add(foodModel);
                    }

                    food.MaxKnownFodColor = Helper.GetMaxKnownProductFodColorString(elements);
                    food.MaxReactionColor = productMaxReactionColor;

                    elementsDict[food.FdcId] = elements;
                }

                ProductVM = new()
                {
                    BrandedResult = productResponse,
                    IngredientsDict = elementsDict,
                    FodmapList = _unitOfWork.Fodmap.GetAll(includeProperties: [Prop.CATEGORY, Prop.COLOR, Prop.MAX_USE_UNITS])
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
