using FoodTracker.Models.NewFolder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.USDA
{
    public class USDABrandedQueryResult
    {
        public int TotalHits {get; set;}
        public int CurrentPage {get; set;}
        public int TotalPages {get; set;}

        public bool Success { get; set; } = true;
        public System.Net.HttpStatusCode Status { get; set; }
        public List<USDAFood> Foods { get; set;}
        public List<Food.Food> CustomFoods { get; set; }
        public FoodSearchCriteria FoodSearchCriteria { get; set; }
    }
}
