using System.ComponentModel.DataAnnotations;

namespace FoodTracker.Models
{
    public class State
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
    }
}
