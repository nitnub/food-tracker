using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Food;
using FoodTracker.Models.USDA;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
using FoodTrackerWeb.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using ZXing;
using ZXing.Windows.Compatibility;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [Area("Guest")]
    [Authorize]
    public partial class ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IUSDAService usdaService) : Controller
    {
        private IUnitOfWork _unitOfWork = unitOfWork;
        private USDABrandedQuery BrandedQuery;
        private ProductVM ProductVM;
        private IUSDAService _usdaService = usdaService;
        private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment;

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

        [HttpPost]
        public ActionResult GetUPC([FromBody] string imageData)
        {
            try 
            { 
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string barcodePath = @"img\bc";
                string finalPath = Path.Combine(wwwRootPath, barcodePath);

                string fileNameWitPath = Path.Combine(finalPath, "0" + ".bmp");
                using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create))
                {
                    using (BinaryWriter bw = new BinaryWriter(fs))
                    {
                        byte[] imgData = Convert.FromBase64String(imageData);
                        bw.Write(imgData);
                        bw.Close();
                    }
                }

                string file = Directory.GetFiles(finalPath)[0];

                    BarcodeReader reader = new BarcodeReader();
                    // load a bitmap
                    var barcodeBitmap = (Bitmap)Image.FromFile(file);

                    // detect and decode the barcode inside the bitmap
                    var result = reader.Decode(barcodeBitmap);

                    if (result == null)
                    {
                        return Json(new { Success = false, Code = "N/A" });
                    }

                    Console.WriteLine(result.Text);
                    Console.WriteLine(result.Text);


                return Json(new { Success = true, Code = result.Text });

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Json(new { Success = false, Code = e.Message });
            }
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
                    return PartialView("_ProductBrandedPartial", new ProductVM() { HasQueryError = true });
                }

                var knownFoodsDict = new Dictionary<string, Food>();
                var elementsDict = new Dictionary<int, ArrayList>();

                var currentTrackedFoods = _unitOfWork.Food.GetAll(f => f.AppUserId == Helper.GetAppUserId(User) || f.IsGlobal, includeProperties: 
                        [Prop.FODMAP_COLOR, Prop.FODMAP_ALIASES, Prop.FODMAP_CATEGORY,Prop.FODMAP_MAX_USE_UNITS, Prop.REACTIONS_SEVERITY, Prop.USER_SAFE_FOODS]);



                foreach (var trackedFood in currentTrackedFoods)
                    knownFoodsDict[trackedFood.Name.ToLower()] = trackedFood;

                foreach (var food in productResponse.Foods)
                {
                    var productMaxReactionColor = "";
                    var elements = new ArrayList();
                    var ingredientEntries = IngredientDelimiters().Split(food.Ingredients);


                    var existingBrandedFood = new Food();
                    if (knownFoodsDict.TryGetValue(food.Description.Trim().ToLower(), out existingBrandedFood))
                    {
                        productMaxReactionColor = Helper.GetMaxSeverityColorString(existingBrandedFood);
                    }
                    else
                    {
                        food.AppFoodId = 0;
                        food.AppFodmapId = 0;
                            food.Id = 0;
                    }

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
                                        productMaxReactionColor == SD.COLOR_GREEN
                                        ? SD.COLOR_GREEN
                                        : (productMaxReactionColor == SD.COLOR_RED || ingredientMaxReactionColor == SD.COLOR_RED) 
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
                    var existingFoodModel = new FoodVM
                    {
                        Food = existingBrandedFood,
                    };

                    food.MaxKnownFodColor = Helper.GetMaxKnownProductFodColorString([..elements, existingFoodModel]);
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
                return PartialView("_ProductBrandedPartial", new ProductVM() { HasQueryError = true });
            }
        }

        [GeneratedRegex(@"([,(){}\[\]])")] // Performance recommendation from VS IDE
        private static partial Regex IngredientDelimiters();
    }
}
