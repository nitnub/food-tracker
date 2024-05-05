namespace FoodTracker.Models.IModel
{
    public interface ISelectable
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public bool Selected { get; set; }
    }
}
