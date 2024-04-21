using FoodTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Meal
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        [ValidateNever]
        public AppUser AppUser { get; set; }

        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int MealTypeId { get; set; }
        [ForeignKey(nameof(MealTypeId))]
        [ValidateNever]
        public MealType MealType { get; set; }

        [ValidateNever]
        public int? ColorId { get; set; }
        [ForeignKey(nameof(ColorId))]
        [ValidateNever]
        public Color Color { get; set; }
        [Required]
        [DisplayName("Time of Meal")]
        public DateTime DateTime { get; set; }

        //[Required, MinLength(1, ErrorMessage = "Meals must contain at least one item")]
        public List<MealItem> MealItems { get; set; } = new List<MealItem>();
        [ValidateNever]
        public List<Reaction.Reaction>? Reactions { get; set; }
    }
}
