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
        public string Name { get; set; }

        public int MealTypeId { get; set; }
        [ForeignKey(nameof(MealTypeId))]
        public MealType MealType { get; set; }
        public DateTime DateTime { get; set; }

        [ValidateNever]
        public List<MealItem> MealItems { get; set; }
    }
}
