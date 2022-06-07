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
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();
                        
            return new NpgsqlConnection(configuration["dbconnection:TourPlannerDB"]);
        }
    }
}
