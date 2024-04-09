using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.IModel
{
    public interface IOwnable
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public bool Global { get; set; }
    }
}
