using FoodTracker.Models.FODMAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Service.IService
{
    public interface IFodmapService
    {
        IEnumerable<Fodmap> GetAll();
    }
}
