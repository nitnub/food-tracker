using FoodTracker.Models.Activity;
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
        [ValidateNever]
        public int? FoodId { get; set; }


        //public int SourceId { get; set; }
        [ValidateNever]
        //public int SourceTypeId { get; set; }
        public ReactionSource SourceTypeId { get; set; }
        [ForeignKey(nameof(SourceTypeId))]
        [ValidateNever]
        public ReactionSourceType SourceType {  get; set; }

        //public int FoodId { get; set; }
        //[ForeignKey(nameof(FoodId))]
        //[ValidateNever]
        //public Food.Food Food { get; set; }



        [ForeignKey(nameof(Meal))]
        [ValidateNever]
        public int? MealId { get; set; }

        
        
        [ForeignKey(nameof(Activity))]
        [ValidateNever]
        public int? ActivityId { get; set; }

        
        
        public int? TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        [ValidateNever]
        public ReactionType Type { get; set; }
        
        
        public int? SeverityId { get; set; } 
        [ForeignKey(nameof(SeverityId))]
        [ValidateNever]
        public ReactionSeverity? Severity { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; } = true;
        public DateTime? IdentifiedOn { get; set; } = DateTime.Now;
        public DateTime? SubsidedOn { get; set;}
    }
}
