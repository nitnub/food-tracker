using FoodTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Reaction
{
    public class UserSafeFood
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        
        //[ForeignKey(nameof(AppUserId))]
        //[ValidateNever]
        //public AppUser AppUser { get; set; }

        [ForeignKey("Food")]
        public int FoodId { get; set; }
        //[ForeignKey(nameof(FoodId))]
        //[ValidateNever]
        //public Food.Food Food { get; set; }
    }
}
