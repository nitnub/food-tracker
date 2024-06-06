using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    public class IconGroupType
    {
        [Key]
        public IconType Id { get; set; }
        public string Name { get; set; }
    }
}
