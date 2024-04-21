using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Meal
{
    public class MealItem
    {
        [Key]
        public int Id { get; set; }
        public int MealId { get; set; }
        //[ForeignKey(nameof(MealId))] // Removed at test
        //[ValidateNever]
        //public Meal Meal { get; set; }
        [Required]
        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        [ValidateNever]
        public Food.Food Food { get; set; }
        [Range(1, Int32.MaxValue)]
        public double Volume {  get; set; }
        public int VolumeUnitsId { get; set; }
        [ForeignKey(nameof(VolumeUnitsId))]
        //[Required]
        [ValidateNever]
        public Unit VolumeUnits { get; set; }
    }
}
