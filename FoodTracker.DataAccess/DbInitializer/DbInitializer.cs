
using FoodTracker.DataAccess.Data;
using FoodTracker.Models.Identity;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FoodTracker.DataAccess.DBInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db, IConfiguration config)
        //public DbInitializer( ApplicationDbContext db, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _config = config;
        }
        public void Initialize()
        {
            // Push migrations if they are not applied.
            try
            {
                if (_db.Database.GetPendingMigrations().Any())
                {
                    _db.Database.Migrate();

                    // Go through all SQL Stage directories and run all queries
                    if (Env.ASPNETCORE_ENVIRONMENT == SD.DEVELOPMENT && Env.SQL_SCRIPT_DIRECTORY != null)
                    {
                        //string connectionString = _config["ConnectionStrings:DefaultConnection"];
                        var directories = Directory.GetDirectories(Env.SQL_SCRIPT_DIRECTORY + @"\populate").ToList();
                        using var conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);

                        foreach (var d in directories)
                        {
                            var files = Directory.GetFiles(d);
                            foreach (var f in files)
                            {
                                string script = File.ReadAllText(f);
                                IEnumerable<string> commandStrings = script.Split(';');

                                conn.Open();
                                foreach (string commandString in commandStrings)
                                {
                                    if (commandString.Trim() != "")
                                    {
                                        using var command = new SqlCommand(commandString, conn);
                                        command.ExecuteNonQuery();
                                    }
                                }
                                conn.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            if (!_roleManager.RoleExistsAsync(SD.ROLE_APP_USER).GetAwaiter().GetResult() && Env.USER_ADMIN_USERNAME != null)
            {
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_APP_USER)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_DELEGATE)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.ROLE_ADMIN)).GetAwaiter().GetResult();

                // If the roles are not created, create admin user
                _userManager.CreateAsync(new AppUser
                {
                    UserName = Env.USER_ADMIN_USERNAME,
                    Email = Env.USER_ADMIN_EMAIL,
                    FirstName = Env.USER_ADMIN_FIRST_NAME,
                    LastName = Env.USER_ADMIN_LAST_NAME
                }, Env.USER_ADMIN_PASSWORD).GetAwaiter().GetResult(); // Must satisfy PW complexity

                AppUser user = _db.AppUsers.FirstOrDefault(u => u.Email == Env.USER_ADMIN_EMAIL);
                _userManager.AddToRoleAsync(user, SD.ROLE_ADMIN).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
