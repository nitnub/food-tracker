using FoodTracker.Models.FODMAP;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Food
{
    public class FoodAlias
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public int FoodId { get; set; }
        //[ForeignKey(nameof(FoodId))]
        //[ValidateNever]
        //public Food Food { get; set; }


        public string Alias { get; set; }

        public bool Global { get; set; } = false;

        [Timestamp]
        [ValidateNever]
        public byte[] Version { get; set; }

    }
}
