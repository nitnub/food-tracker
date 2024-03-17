﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.FODMAP
{
    public class Fodmap
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int ColorId { get; set; }
        [ForeignKey(nameof(ColorId))] // TODO: verify
        [ValidateNever]
        public Color Color { get; set; }

        public bool FreeUse {  get; set; }
        public int MaxUse {  get; set; }

        public int MaxUseUnitsId {  get; set; }
        [ForeignKey(nameof(MaxUseUnitsId))] // TODO: verify
        [ValidateNever]
        public Unit MaxUseUnits {  get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public FodmapCategory Category {  get; set; }

        public bool Oligos {  get; set; }
        public bool Fructose {  get; set; }
        public bool Polyols {  get; set; }
        public bool Lactose {  get; set; }


    }
}
