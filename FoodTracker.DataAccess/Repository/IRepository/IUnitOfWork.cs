using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IActivityRepository Activity { get; }
        IActivityIntensityRepository ActivityIntensity { get; }
        IActivityTypeRepository ActivityType { get; }
        IAppUserRepository AppUser { get; }
        IColorRepository Color { get; }
        IEventRepository Event { get; }
        IEventTypeRepository EventType { get; }
        IFodmapAliasRepository FodmapAlias { get; }
        IFodmapCategoryRepository FodmapCategory { get; }
        IFodmapRepository Fodmap { get; }
        IFoodRepository Food { get; }
        IFoodAliasRepository FoodAlias { get; }
        IIngredientMapRepository IngredientMap { get; }
        ILocationRepository Location { get; }
        IReactionCategoryRepository ReactionCategory { get; }
        IReactionRepository Reaction { get; }
        IReactionSeverityRepository ReactionSeverity { get; }
        IReactionTypeRepository ReactionType { get; }
        IStateRepository State { get; }
        IUnitRepository Unit { get; }

        void Save();
    }
}
