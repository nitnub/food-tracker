using FoodTracker.Models.IModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Food
{
    public class FoodAlias : IOwnable
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public int FoodId { get; set; }
        public string Alias { get; set; }
        public bool IsGlobal { get; set; } = false;

        [Timestamp]
        [ValidateNever]
        public byte[] Version { get; set; }

    }
}
