using FoodTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Reaction
{
    public class Reaction  
    {
        // TODO: Element (food or environment to extend "Allergen")
        [Key]
        public int Id { get; set; }
        // TODO: User
        public string AppUserId {  get; set; }
        [ForeignKey(nameof(AppUserId))]
        [ValidateNever]
        public AppUser AppUser { get; set; }


        [ForeignKey(nameof(Food))]
        public int FoodId { get; set; }



        //public int FoodId { get; set; }
        //[ForeignKey(nameof(FoodId))]
        //[ValidateNever]
        //public Food.Food Food { get; set; }





        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        [ValidateNever]
        public ReactionType Type { get; set; }
        public int SeverityId { get; set; } 
        [ForeignKey(nameof(SeverityId))]
        [ValidateNever]
        public ReactionSeverity Severity { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
        public DateTime? IdentifiedOn { get; set; }
        public DateTime? SubsidedOn { get; set;}
    }
}
