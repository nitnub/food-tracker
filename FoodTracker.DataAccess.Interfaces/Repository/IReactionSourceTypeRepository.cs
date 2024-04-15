﻿using FoodTracker.Models.Activity;
using FoodTracker.Models.Reaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IReactionTypeRepository : IRepository<ReactionType>
    {
     public void Update(ReactionType obj);
    }
}
