using FoodTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Reaction
{
    public class Reaction  
    {
        [Key]
        public int Id { get; set; }
        public string AppUserId {  get; set; }
        [ForeignKey(nameof(AppUserId))]
        [ValidateNever]
        public AppUser AppUser { get; set; }
        [ForeignKey(nameof(Food))]
        [ValidateNever]
        public int? FoodId { get; set; }

        
        [ValidateNever]
        public ReactionSource SourceTypeId { get; set; }
        [ForeignKey(nameof(SourceTypeId))]
        [ValidateNever]
        public ReactionSourceType SourceType {  get; set; }
        

        [ValidateNever]
        public int? MealId { get; set; }
        [ForeignKey(nameof(MealId))]
        [ValidateNever]
        public Meal.Meal Meal { get; set; }


        [ValidateNever]
        public int? ActivityId { get; set; }
        [ForeignKey(nameof(ActivityId))]
        [ValidateNever]
        public Activity.Activity Activity { get; set; }


        public int? TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        [ValidateNever]
        public ReactionType Type { get; set; }

        [ValidateNever]
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
