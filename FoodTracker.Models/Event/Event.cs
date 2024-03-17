using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Models.Event
{
    // Ex: Meal / Activity
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public int EventTypeId { get; set; }

        [ForeignKey(nameof(EventTypeId))]
        [ValidateNever]
        public EventType EventType { get; set; }

        public DateTime Time { get; set; }

        // TODO: app user property
        //[ForeignKey(nameof(AppUser))]
        //public string AppUser {  get; set; }

    }
}
