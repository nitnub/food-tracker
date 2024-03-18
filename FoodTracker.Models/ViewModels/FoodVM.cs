using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.ViewModels
{
    public class FoodVM
    {
        public Food.Food Food { get; set; }
        [ValidateNever]
        public IEnumerable<Food.Food> FoodList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> FodmapList_OLD { get; set; }

        [ValidateNever]
        public IEnumerable<Fodmap> FodmapList { get; set; }
        public Fodmap FodmapSelected { get; set; } 

        
        //public int FoodID { get; set; }
    }
}
