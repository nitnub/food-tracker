using FoodTracker.Models.Activity;
using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IReactionCategoryRepository : IRepository<ReactionCategory>
    {
     public void Update(ReactionCategory obj);
    }
}
