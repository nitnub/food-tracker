using FoodTracker.Models.FODMAP;
using FoodTracker.Models.USDA;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models.ViewModels
{
    public class ProductVM
    {
        public USDABrandedQueryResult BrandedResult {  get; set; }
        [DisplayName("Product name or UPC")]
        [Required]
        [EmailAddress]
        public string QueryString { get; set; }
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
