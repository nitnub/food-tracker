using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    public class UnitType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
