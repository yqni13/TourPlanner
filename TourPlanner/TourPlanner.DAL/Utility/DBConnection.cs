using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DAL.Utility
{
    class DBConnection
    {
        
        public static IDbConnection GetConnection()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json.config", optional: false, reloadOnChange: true)
                .Build();

            //return new NpgsqlConnection("Host=localhost;Username=swen2;Password=swen2;Database=tourplanner");
            return new NpgsqlConnection(configuration["dbconnection:TourPlannerDB"]);
        }
    }
}
