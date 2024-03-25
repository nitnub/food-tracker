using FoodTracker.Models;
using FoodTracker.Models.Activity;
using FoodTracker.Models.Event;
using FoodTracker.Models.FODMAP;
using FoodTracker.Models.Food;
using FoodTracker.Models.Identity;
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
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Fodmap> Fodmaps { get; set; }
        public DbSet<FodmapAlias> FodmapAliases { get; set; }
        public DbSet<FodmapCategory> FodmapCategories { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<FoodAlias> FoodAliases { get; set; }
        public DbSet<IngredientMap> IngredientMaps { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealItem> MealItems { get; set; }
        public DbSet<ReactionCategory> ReactionCategories { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<ReactionSeverity> ReactionSeverities { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Unit> Units { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


            //modelBuilder.Entity<Reaction>().HasData(
            //    new Reaction
            //    {
            //        Id = 1,
            //        AppUserId = "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9",
            //        FoodId = 1,
            //        TypeId = 1,
            //        SeverityId = 3,
            //        Active = true
            //    },
            //    new Reaction
            //    {
            //        Id = 2,
            //        AppUserId = "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9",
            //        FoodId = 1,
            //        TypeId = 2,
            //        SeverityId = 2,
            //        Active = true
            //    },
            //    new Reaction
            //    {
            //        Id = 3,
            //        AppUserId = "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9",
            //        FoodId = 2,
            //        TypeId = 4,
            //        SeverityId = 2,
            //        Active = true
            //    },
            //    new Reaction
            //    {
            //        Id = 4,
            //        AppUserId = "ee5af4ea-6a83-42c7-8f7b-5b1fc16c58c9",
            //        FoodId = 1,
            //        TypeId = 7,
            //        SeverityId = 3,
            //        Active = true
            //    });

            modelBuilder.Entity<State>().HasData(
                new State { Id = 1, Name = "Alabama", Abbreviation = "AL" },
                new State { Id = 2, Name = "Alaska", Abbreviation = "AK" },
                new State { Id = 3, Name = "Arizona", Abbreviation = "AZ" },
                new State { Id = 4, Name = "Arkansas", Abbreviation = "AR" },
                new State { Id = 5, Name = "American Samoa", Abbreviation = "AS" },
                new State { Id = 6, Name = "California", Abbreviation = "CA" },
                new State { Id = 7, Name = "Colorado", Abbreviation = "CO" },
                new State { Id = 8, Name = "Connecticut", Abbreviation = "CT" },
                new State { Id = 9, Name = "Delaware", Abbreviation = "DE" },
                new State { Id = 10, Name = "District of Columbia", Abbreviation = "DC" },
                new State { Id = 11, Name = "Florida", Abbreviation = "FL" },
                new State { Id = 12, Name = "Georgia", Abbreviation = "GA" },
                new State { Id = 13, Name = "Guam", Abbreviation = "GU" },
                new State { Id = 14, Name = "Hawaii", Abbreviation = "HI" },
                new State { Id = 15, Name = "Idaho", Abbreviation = "ID" },
                new State { Id = 16, Name = "Illinois", Abbreviation = "IL" },
                new State { Id = 17, Name = "Indiana", Abbreviation = "IN" },
                new State { Id = 18, Name = "Iowa", Abbreviation = "IA" },
                new State { Id = 19, Name = "Kansas", Abbreviation = "KS" },
                new State { Id = 20, Name = "Kentucky", Abbreviation = "KY" },
                new State { Id = 21, Name = "Louisiana", Abbreviation = "LA" },
                new State { Id = 22, Name = "Maine", Abbreviation = "ME" },
                new State { Id = 23, Name = "Maryland", Abbreviation = "MD" },
                new State { Id = 24, Name = "Massachusetts", Abbreviation = "MA" },
                new State { Id = 25, Name = "Michigan", Abbreviation = "MI" },
                new State { Id = 26, Name = "Minnesota", Abbreviation = "MN" },
                new State { Id = 27, Name = "Mississippi", Abbreviation = "MS" },
                new State { Id = 28, Name = "Missouri", Abbreviation = "MO" },
                new State { Id = 29, Name = "Montana", Abbreviation = "MT" },
                new State { Id = 30, Name = "Nebraska", Abbreviation = "NE" },
                new State { Id = 31, Name = "Nevada", Abbreviation = "NV" },
                new State { Id = 32, Name = "New Hampshire", Abbreviation = "NH" },
                new State { Id = 33, Name = "New Jersey", Abbreviation = "NJ" },
                new State { Id = 34, Name = "New Mexico", Abbreviation = "NM" },
                new State { Id = 35, Name = "New York", Abbreviation = "NY" },
                new State { Id = 36, Name = "North Carolina", Abbreviation = "NC" },
                new State { Id = 37, Name = "North Dakota", Abbreviation = "ND" },
                new State { Id = 38, Name = "Northern Mariana Islands", Abbreviation = "MP" },
                new State { Id = 39, Name = "Ohio", Abbreviation = "OH" },
                new State { Id = 40, Name = "Oklahoma", Abbreviation = "OK" },
                new State { Id = 41, Name = "Oregon", Abbreviation = "OR" },
                new State { Id = 42, Name = "Pennsylvania", Abbreviation = "PA" },
                new State { Id = 43, Name = "Puerto Rico", Abbreviation = "PR" },
                new State { Id = 44, Name = "Rhode Island", Abbreviation = "RI" },
                new State { Id = 45, Name = "South Carolina", Abbreviation = "SC" },
                new State { Id = 46, Name = "South Dakota", Abbreviation = "SD" },
                new State { Id = 47, Name = "Tennessee", Abbreviation = "TN" },
                new State { Id = 48, Name = "Texas", Abbreviation = "TX" },
                new State { Id = 49, Name = "Trust Territories", Abbreviation = "TT" },
                new State { Id = 50, Name = "Utah", Abbreviation = "UT" },
                new State { Id = 51, Name = "Vermont", Abbreviation = "VT" },
                new State { Id = 52, Name = "Virginia", Abbreviation = "VA" },
                new State { Id = 53, Name = "Virgin Islands", Abbreviation = "VI" },
                new State { Id = 54, Name = "Washington", Abbreviation = "WA" },
                new State { Id = 55, Name = "West Virginia", Abbreviation = "WV" },
                new State { Id = 56, Name = "Wisconsin", Abbreviation = "WI" },
                new State { Id = 57, Name = "Wyoming", Abbreviation = "WY" });
        }
    }
}
