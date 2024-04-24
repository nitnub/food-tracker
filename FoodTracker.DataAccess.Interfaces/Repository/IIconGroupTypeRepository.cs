using FoodTracker.Models;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IIconGroupTypeRepository : IRepository<IconGroupType>
    {
     public void Update(IconGroupType obj);
    }
}
