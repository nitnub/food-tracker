using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Activity
{
    public class Activity
    {
        [Key]
        public int Id { get; set; }
        
        public int TypeId { get; set; }
        [ForeignKey(nameof(TypeId))]
        [ValidateNever]
        public ActivityType Type { get; set; }
        public int Duration { get; set; }

        public int DurationUnitId { get; set; }
        [ForeignKey(nameof(DurationUnitId))]
        [ValidateNever]
        public Unit DurationUnits { get; set; }
        
        public int IntensityId { get; set; }
        [ForeignKey(nameof(IntensityId))]
        [ValidateNever]
        public ActivityIntensity Intensity { get; set; }

        public int LocationId { get; set; }
        [ForeignKey(nameof(LocationId))]
        [ValidateNever]
        public Location Location { get; set; }

        // had event map previously
    }
}
