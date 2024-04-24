using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodTracker.Models.Day
{
    public class UserSafeDay
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }       
        public DateOnly Date { get; set; }

    }
}
