using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Event
{
    public class EventType
    {
        [Key]
        public EventGroup Id { get; set; }
        public string Name { get; set; }
    }
}
