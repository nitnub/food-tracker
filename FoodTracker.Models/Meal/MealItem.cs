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
        [ForeignKey(nameof(MealId))]
        public Meal Meal { get; set; }
        public int FoodId { get; set; }
        [ForeignKey(nameof(FoodId))]
        public Food.Food Food { get; set; }
        public double Volume {  get; set; }
        public Unit VolumeUnits { get; set; }
    }
}
