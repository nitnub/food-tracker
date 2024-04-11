using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int Type { get; set; }
    }
}
