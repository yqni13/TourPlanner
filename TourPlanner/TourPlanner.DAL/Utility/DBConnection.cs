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
            return new NpgsqlConnection("Host=localhost;Username=swen2;Password=swen2;Database=tourplanner");
        }
    }
}
