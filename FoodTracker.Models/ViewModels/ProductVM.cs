using FoodTracker.Models.FODMAP;
using FoodTracker.Models.USDA;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        public bool HasQueryError { get; set; } = false;
        public string QueryErrorMessage { get; set; } = "Error processing query. Please try again later.";

        public FoodVM? FoodVM { get; set; }
        [ValidateNever]
        public string? MaxReactionColor { get; set; }

        [ValidateNever]
        public IEnumerable<Fodmap> FodmapList { get; set; }
        public string UpcImageUrl { get; set; }

    }
}
