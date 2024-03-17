
using FoodTracker.DataAccess.Data;
using FoodTracker.Utility;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace FoodTracker.DataAccess.DBInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _config;
        public DbInitializer(ApplicationDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public void Initialize()
        {
            // Push migrations if they are not applied.
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();

                    // Read files
                    if (Env.ASPNETCORE_ENVIRONMENT == "Development" && Env.SQL_SCRIPT_DIRECTORY != null)
                    {
                        string connectionString = _config["ConnectionStrings:DefaultConnection"];

                        var directories = Directory.GetDirectories(Env.SQL_SCRIPT_DIRECTORY + @"\populate").ToList();

                        using var conn = new SqlConnection(connectionString);

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
                throw;
            }

            return;
        }
    }
}
