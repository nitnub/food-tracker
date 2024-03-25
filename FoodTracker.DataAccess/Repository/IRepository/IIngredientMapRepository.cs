using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IIngredientMapRepository : IRepository<IngredientMap>
    {
        public void Update(IngredientMap obj);
    }
}
