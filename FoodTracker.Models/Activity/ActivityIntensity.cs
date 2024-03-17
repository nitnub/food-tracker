using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Activity
{
    public class ActivityIntensity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value {  get; set; }
    }
}
