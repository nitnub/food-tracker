using FoodTracker.DataAccess.Interfaces;
using FoodTracker.Models.Day;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.IModel;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public bool Vegetarian { get; set; }

        [DefaultValue(false)]
        public bool Vegan { get; set; }

        [DefaultValue(false)]
        public bool GlutenFree { get; set; }

        [InverseProperty(nameof(IngredientMap.ParentFood))]
        public ICollection<IngredientMap>? ParentFoods { get; set; }

        [InverseProperty(nameof(IngredientMap.IngredientFood))]
        public ICollection<IngredientMap>? IngredientFoods { get; set; }
        public IEnumerable<FoodAlias>? Aliases { get; set; }
        public IEnumerable<UserSafeFood>? UserSafeFoods { get; set; }
        public IEnumerable<Reaction.Reaction>? Reactions { get; set; }

        public bool Global { get; set; } = false;
    }
}
