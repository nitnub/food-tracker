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
    public class ReactionCategory
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ValidateNever]
        public int IconId { get; set; }
        [ForeignKey(nameof(IconId))]
        [ValidateNever]
        public Icon Icon {  get; set; }
    }
}
