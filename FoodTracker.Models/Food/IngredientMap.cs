using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Food
{
    public class IngredientMap
    {
        [Key]
        public int Id { get; set; }
        //public int ParentFoodId { get; set; }
        //// Potential circular reference issues..
        //[ForeignKey(nameof(ParentFoodId))]
        //[ValidateNever]
        //[Required]
        public Food? ParentFood { get; set; }

        // had ParentGrouping
        //public int IngredientFoodId { get; set; }

        //[ForeignKey(nameof(IngredientFoodId))]
        //[ValidateNever]
        //[Required]
        public Food? IngredientFood { get; set; }
    }
}
