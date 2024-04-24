﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
    public class Icon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string HTML { get; set; }

        [Column("IconGroupTypeId")]
        public IconType Type { get; set; }
        [ForeignKey(nameof(Type))]
        [ValidateNever]
        public IconGroupType IconGroupType { get; set; }
    }
}
