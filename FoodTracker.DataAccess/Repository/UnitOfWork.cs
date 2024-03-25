using FoodTracker.DataAccess.Data;
using FoodTracker.DataAccess.Repository.IRepository;
using FoodTracker.Models.Activity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;


        public IActivityRepository Activity { get; set; }
        public IActivityIntensityRepository ActivityIntensity { get; set; }
        public IActivityTypeRepository ActivityType { get; set; }
        public IAppUserRepository AppUser { get; set; }
        public IColorRepository Color { get; set; }
        public IEventRepository Event { get; set; }
        public IEventTypeRepository EventType { get; set; }
        public IFodmapAliasRepository FodmapAlias { get; set; }
        public IFodmapCategoryRepository FodmapCategory { get; set; }
        public IFodmapRepository Fodmap { get; set; }
        public IFoodAliasRepository FoodAlias { get; set; }
        public IFoodRepository Food { get; set; }
        public IIngredientMapRepository IngredientMap { get; set; }
        public ILocationRepository Location { get; set; }
        public IMealRepository Meal { get; set; }
        public IMealItemRepository MealItem { get; set; }
        public IReactionCategoryRepository ReactionCategory { get; set; }
        public IReactionRepository Reaction { get; set; }
        public IReactionSeverityRepository ReactionSeverity { get; set; }
        public IReactionTypeRepository ReactionType { get; set; }
        public IStateRepository State { get; set; }
        public IUnitRepository Unit { get; set; }

        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Activity = new ActivityRepository(_db);
            ActivityIntensity = new ActivityIntensityRepository(_db);
            ActivityType = new ActivityTypeRepository(_db);
            AppUser = new AppUserRepository(_db);
            Color = new ColorRepository(_db);
            Event = new EventRepository(_db);
            EventType = new EventTypeRepository(_db);
            FodmapAlias = new FodmapAliasRepository(_db);
            FodmapCategory = new FodmapCategoryRepository(_db);
            Fodmap = new FodmapRepository(_db);
            Food = new FoodRepository(_db);
            FoodAlias = new FoodAliasRepository(_db);
            IngredientMap = new IngredientMapRepository(_db);
            Location = new LocationRepository(_db);
            Meal = new MealRepository(_db);
            MealItem = new MealItemRepository(_db);
            ReactionCategory = new ReactionCategoryRepository(_db);
            Reaction = new ReactionRepository(_db);
            ReactionSeverity = new ReactionSeverityRepository(_db);
            ReactionType = new ReactionTypeRepository(_db);
            State = new StateRepository(_db);
            Unit = new UnitRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
