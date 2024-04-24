using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Interfaces
{
    public interface IReactable
    {
        public IEnumerable <Reaction> Reactions { get; set; }
    }
}
