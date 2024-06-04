using FoodTracker.DataAccess.Interfaces;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.IModel;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Food
{



    public class Food : IOwnable, IReactable
    {
        [Key]
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        [StringLength(60, MinimumLength = 1)]
        [Required]
        public string Name { get; set; }
        public int? FodmapId { get; set; }

        [ForeignKey(nameof(FodmapId))]
        [ValidateNever]
        public Fodmap Fodmap { get; set; }

        [DefaultValue(false)]
        [DisplayName("Vegetarian")]
        public bool IsVegetarian { get; set; }

        [DefaultValue(false)]
        [DisplayName("Vegan")]
        public bool IsVegan { get; set; }

        [DefaultValue(false)]
        [DisplayName("Gluten")]
        public bool IsGlutenFree { get; set; }

        [InverseProperty(nameof(IngredientMap.ParentFood))]
        public ICollection<IngredientMap>? ParentFoods { get; set; }

        [InverseProperty(nameof(IngredientMap.IngredientFood))]
        public ICollection<IngredientMap>? IngredientFoods { get; set; }
        public IEnumerable<FoodAlias>? Aliases { get; set; }
        public IEnumerable<UserSafeFood>? UserSafeFoods { get; set; }
        public IEnumerable<Reaction.Reaction>? Reactions { get; set; }

        public bool IsGlobal { get; set; } = false;
    }
}
