﻿using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models.ViewModels
{
    public class MealVM
    {
        [Required]
        public Meal.Meal Meal {  get; set; }
        [ValidateNever]
        public IEnumerable<Color> ColorOptions { get; set; }
        [ValidateNever]
        public IEnumerable<MealType> MealTypes { get; set; }
        [ValidateNever]
        public IEnumerable<Food.Food> Foods { get; set; }
        [ValidateNever]
        public IEnumerable<Unit> Units { get; set; }
        public Dictionary<string, MealItem> MealItems { get; set; }
        [ValidateNever]
        public Dictionary<string, List<ReactionType>> Categories { get; set; }
        [ValidateNever]
        public Dictionary<int, bool> Reactions { get; set; }
        public IEnumerable<SelectListItem>? MealTemplates { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CalendarDate { get; set; }
        public int TemplateId { get; set; }
        public string? UserTimeZone { get; set; }
    }
}
