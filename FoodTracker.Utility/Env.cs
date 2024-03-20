using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTracker.Utility
{
    public class Env
    {
        public static string ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        public static string SQL_SCRIPT_DIRECTORY = Environment.GetEnvironmentVariable("SQL_SCRIPT_DIRECTORY");

        // Default Admin
        public static string USER_ADMIN_USERNAME = Environment.GetEnvironmentVariable("USER_ADMIN_USERNAME");
        public static string USER_ADMIN_EMAIL = Environment.GetEnvironmentVariable("USER_ADMIN_EMAIL");
        public static string USER_ADMIN_FIRST_NAME = Environment.GetEnvironmentVariable("USER_ADMIN_FIRST_NAME");
        public static string USER_ADMIN_LAST_NAME = Environment.GetEnvironmentVariable("USER_ADMIN_LAST_NAME");
        public static string USER_ADMIN_PASSWORD = Environment.GetEnvironmentVariable("USER_ADMIN_PASSWORD");
    }
}
