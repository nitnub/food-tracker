﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models
{
    public class IconGroupType
    {
        [Key]
        public IconType Id { get; set; }
        public string Name { get; set; }

    }
}
