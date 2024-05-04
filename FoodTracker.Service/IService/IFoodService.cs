using FoodTracker.Models.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Service.IService
{
    public interface IFoodService
    {
        IEnumerable<Food> GetAll();
        IEnumerable<Food> GetAllSorted();
        Food Get(int id);
        Food Get(string name);
        Food GetNewFood();
        void Upsert(Food food);
        bool Remove(int? id);
    }
}
