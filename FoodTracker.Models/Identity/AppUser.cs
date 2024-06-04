using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Identity
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }
        public string? StreetAddressOne { get; set; }
        public string? StreetAddressTwo { get; set; }
        public string? City { get; set; }
        public int? StateId { get; set; }
        [ForeignKey(nameof(StateId))]
        [ValidateNever]
        public State? State { get; set; }
        public string? PostalCode { get; set; }

        [NotMapped] 
        public string Role { get; set; }

        [NotMapped]
        public IEnumerable<Reaction.Reaction> Reactions { get; set; }
    }
}
