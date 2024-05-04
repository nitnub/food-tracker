using FoodTracker.Models.FODMAP;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models.ViewModels
{
    public class FoodVM
    {
        [Required]
        public Food.Food Food { get; set; }
        [ValidateNever]
        public IEnumerable<Food.Food> FoodList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> FodmapList_OLD { get; set; }

        [ValidateNever]
        public IEnumerable<Fodmap> FodmapList { get; set; }
        public Fodmap FodmapSelected { get; set; }

        public double MaxReaction { get; set; } = -1;
        public string MaxReactionColor { get; set; }
    }
}
