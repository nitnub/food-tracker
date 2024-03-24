using Azure;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models;
using FoodTracker.Models.Food;
using FoodTracker.Models.NewFolder;
using FoodTracker.Models.USDA;
using FoodTracker.Models.ViewModels;
using FoodTracker.Utility;
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
    public partial class ProductController(IUnitOfWork unitOfWork) : Controller
    {
        private IUnitOfWork _unitOfWork = unitOfWork;
        private USDABrandedQuery BrandedQuery;
        private ProductVM ProductVM;
        private static HttpClient client = new(); // needs to be static!
        public IActionResult Index()
        {
            ProductVM = new();
            return View(ProductVM);
        }

        public async Task<IActionResult> GetUSDAProducts(string userQuery)
        {

            var productResponse = await GetUSDAProductsAPI(userQuery);

            var knownFoodsDict = new Dictionary<string, Food>();
            var elementsDict = new Dictionary<int, ArrayList>();

            var currentTrackedFoods = _unitOfWork.Food.GetAll(includeProperties: "Fodmap.Color,Fodmap.Aliases,Fodmap.Category,Fodmap.MaxUseUnits");

            foreach (var trackedFood in currentTrackedFoods)
                knownFoodsDict[trackedFood.Name.ToLower()] = trackedFood;
            
            foreach (var food in productResponse.Foods)
            {
                var elements = new ArrayList();

                string[] ingredientEntries = IngredientDelimiters().Split(food.Ingredients);
                foreach (string item in ingredientEntries)
                {
                    if  (item.Equals(",") || item.Equals(".") || item.Length == 0) continue;

                    if (item.Length == 1) 
                    { 
                        elements.Add(item);
                        continue;
                    }

                    var existingFood = new Food();
                    if (!knownFoodsDict.TryGetValue(item.Trim().ToLower(), out existingFood))
                        existingFood = new Food() { Name = item.Trim() };

                    elements.Add(existingFood);
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

        
        public async Task<USDABrandedQueryResult> GetUSDAProductsAPI(string userQuery)
        {
            int pageSize = 25;
            int pageNumber = 1;
            string sortBy = "dataType.keyword";
            string sortOrder = "asc";

            var query = new Dictionary<string, string>
            {
                { "query", userQuery },
                { "pageSize", $"{pageSize}" },
                { "pageNumber", $"{pageNumber}" },
                { "sortBy", sortBy },
                { "sortOrder", sortOrder },
                { "api_key", Env.USDA_API_KEY}
            };

            var builder = new UriBuilder(SD.USDA_URL_SEARCH_GET)
            {
                Port = -1,
                Query = QueryString.Create(query).ToString()
            };

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(builder.ToString()),
                Method = HttpMethod.Get,
            };

            var response = await client.SendAsync(request);

            return await response.Content.ReadAsAsync<USDABrandedQueryResult>();
        }

        [GeneratedRegex(@"([,(){}\[\]])")] // Performance recommendation from VS IDE
        private static partial Regex IngredientDelimiters(); 
    }
}
