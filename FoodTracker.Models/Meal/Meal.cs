using FoodTracker.Models.Identity;
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
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        [ValidateNever]
        public AppUser AppUser { get; set; }

        public string Name { get; set; }

        public int MealTypeId { get; set; }
        [ForeignKey(nameof(MealTypeId))]
        [ValidateNever]
        public MealType MealType { get; set; }
        public DateTime DateTime { get; set; }

        [ValidateNever]
        public List<MealItem> MealItems { get; set; }
    }
}
