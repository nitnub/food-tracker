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
    }
}
