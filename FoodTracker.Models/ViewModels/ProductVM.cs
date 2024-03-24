using FoodTracker.Models.USDA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class ProductVM
    {
        public USDABrandedQueryResult BrandedResult {  get; set; }
        [DisplayName("Product name or UPC")]
        [Required]
        [EmailAddress]
        public string QueryString { get; set; }

        //public Dictionary<int, List<Food.Food>> FoodInteractions { get; set; }
        public Dictionary<int, ArrayList> IngredientsDict { get; set; }

    }
}
