using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.FODMAP
{
    public class FodmapAlias
    {
        [Key]
        public int Id { get; set; }
        public string Alias { get; set; }
        public int FodmapId { get; set; }

        [ForeignKey(nameof(FodmapId))]
        [ValidateNever]
        public Fodmap Fodmap { get; set; }

        // TODO: redundant with above?
        public string OriginalName { get; set; }
        public bool IsPrimary { get; set; }
    }
}
