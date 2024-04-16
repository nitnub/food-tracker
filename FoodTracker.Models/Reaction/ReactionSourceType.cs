using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Reaction
{
    public class ReactionSourceType
    {
        [Key]
        public ReactionSource Id { get; set; }
        public string Name { get; set; }
        //public int Id { get; set; }
        //public ReactionSource Name { get; set; }
    }
}
