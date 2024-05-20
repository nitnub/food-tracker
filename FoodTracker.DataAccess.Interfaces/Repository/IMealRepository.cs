using FoodTracker.Models.Meal;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IMealRepository : IRepository<Meal>
    {
     public void Update(Meal obj);
    }
}
