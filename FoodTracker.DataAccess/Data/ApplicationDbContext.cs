using FoodTracker.Models;
using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using FoodTracker.Models.Meal;
using FoodTracker.Models.Reaction;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FoodTracker.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityIntensity> ActivityIntensities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Fodmap> Fodmaps { get; set; }
        public DbSet<FodmapAlias> FodmapAliases { get; set; }
        public DbSet<FodmapCategory> FodmapCategories { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<IngredientMap> IngredientMaps { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<ReactionCategory> ReactionCategories { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<ReactionSeverity> ReactionSeverities { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<Unit> Units { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);



            //modelBuilder.Entity<Food>().HasData(

            //    );

           


        }
    }
}
