using FoodTracker.Models;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IIconRepository : IRepository<Icon>
    {
     public void Update(Icon obj);
    }
}
