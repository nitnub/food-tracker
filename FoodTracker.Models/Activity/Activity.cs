using FoodTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Activity
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever] 
        public string AppUserId { get; set; }
        [ForeignKey(nameof(AppUserId))]
        [ValidateNever]
        public AppUser AppUser { get; set; }

        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        [ValidateNever]
        public ActivityType ActivityType { get; set; }
        public TimeSpan Duration { get; set; }
        //public DateTime StartTime { get; set; }

        //public TimeSpan DurationTS { get; set; }

        //public int DurationUnitId { get; set; }
        //[ForeignKey(nameof(DurationUnitId))]
        //[ValidateNever]
        //public Unit DurationUnits { get; set; }
        
        public int IntensityId { get; set; }
        [ForeignKey(nameof(IntensityId))]
        [ValidateNever]
        public ActivityIntensity Intensity { get; set; }

        //public int LocationId { get; set; }
        //[ForeignKey(nameof(LocationId))]
        //[ValidateNever]
        //public Location Location { get; set; }

        //public int EventId { get; set; }
        //[ForeignKey(nameof(EventId))]
        //[ValidateNever]
        //public Event.Event Event { get; set; }
        [Required]
        [DisplayName("Start Time")]
        public DateTime DateTime { get; set; }

    }
}
