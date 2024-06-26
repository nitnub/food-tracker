﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models
{
    public class Unit
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }
        public string ShortName { get; set; }
        public string ShortNamePlural { get; set; }

        public int UnitTypeId { get; set; }

        [ForeignKey(nameof(UnitTypeId))]
        [ValidateNever]
        public UnitType UnitType { get; set; }
    }
}
