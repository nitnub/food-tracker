using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Meal
{
    public class Meal
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<MealItem> MealItems { get; set; }
    }
}
