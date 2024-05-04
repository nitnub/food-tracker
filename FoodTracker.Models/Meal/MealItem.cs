using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Meal
{
    public class MealItem
    {
        [Key]
        public int Id { get; set; }
        public int MealId { get; set; }
        [Required]
        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        [ValidateNever]
        public Food.Food Food { get; set; }
        [Range(1, int.MaxValue)]
        public double Volume {  get; set; }
        public int VolumeUnitsId { get; set; }
        [ForeignKey(nameof(VolumeUnitsId))]
        //[Required]
        [ValidateNever]
        public Unit VolumeUnits { get; set; }
    }
}
