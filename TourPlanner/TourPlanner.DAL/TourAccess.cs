using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL.Utility;
using TourPlanner.Models;

namespace TourPlanner.DAL
{
    public class TourAccess
    {

        private string _tableNameTours = "tours";
        private string _tableNameTourLogs = "tourlogs";

        public static void AddTour()
        {
            
                //with the using statement we make sure the connection gets freed right after the block is done
                using (IDbConnection connection = DBConnection.GetConnection())
                {
                    //open the DB connection and create a command
                    connection.Open();
                    IDbCommand command = connection.CreateCommand();

                    command.CommandText = @"
                    INSERT INTO tours 
                    (t_name, t_description, t_startname, t_endname, t_startcoord, t_endcoord, t_transporttype, t_distance, t_estimatetime, t_mappath)
                    VALUES (@t_name, @t_description, @t_startname, @t_endname, @t_startcoord, @t_endcoord, @t_transporttype, @t_distance, @t_estimatetime, @t_mappath)              
                    ";

                    NpgsqlCommand c = command as NpgsqlCommand;

                    c.Parameters.Add("t_name", NpgsqlDbType.Varchar, 50);
                    c.Parameters.Add("t_description", NpgsqlDbType.Varchar, 50);
                    c.Parameters.Add("t_startname", NpgsqlDbType.Varchar, 50);
                    c.Parameters.Add("t_endname", NpgsqlDbType.Varchar, 50);

                    c.Parameters.Add("t_startcoord", NpgsqlDbType.Integer, 50);
                    c.Parameters.Add("t_endcoord", NpgsqlDbType.Integer, 50);
                    c.Parameters.Add("t_transporttype", NpgsqlDbType.Integer, 50);
                    c.Parameters.Add("t_distance", NpgsqlDbType.Integer, 50);

                    c.Parameters.Add("t_estimatetime", NpgsqlDbType.Time, 50);

                    c.Parameters.Add("t_mappath", NpgsqlDbType.Varchar, 50);

                    c.Prepare();

                    c.Parameters["t_name"].Value = "test";
                    c.Parameters["t_description"].Value = "test";
                    c.Parameters["t_startname"].Value = "test";
                    c.Parameters["t_endname"].Value = "test";

                    c.Parameters["t_startcoord"].Value = 123;
                    c.Parameters["t_endcoord"].Value = 321;
                    c.Parameters["t_transporttype"].Value = 1;
                    c.Parameters["t_distance"].Value = 15;

                    c.Parameters["t_estimatetime"].Value = new TimeSpan(2,23,5);

                    c.Parameters["t_mappath"].Value = "test";

                    command.ExecuteNonQuery();
                }
        }

        public static List<Tour> AccessTours()
        {
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                List<Tour> tours = new();

                connection.Open();
                IDbCommand command = connection.CreateCommand();
                command.CommandText = @"SELECT * FROM {_tableNameTours}";
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Guid id = Guid.Parse(reader[0].ToString());
                    string name = reader.GetString(1);
                    string description = reader.GetString(2);
                    Adress from = (Adress)reader.GetValue(3);
                    Adress to = (Adress)reader.GetValue(4);
                    double startC = reader.GetDouble(5);
                    double endC = reader.GetDouble(6);
                    ETransportType transport = (ETransportType)reader.GetValue(7);
                    double distance = reader.GetDouble(8);
                    TimeSpan duration = (TimeSpan)reader.GetValue(9);
                    string mappath = reader.GetString(10);

                    tours.Add(new Tour(
                        id,
                        name,
                        description,
                        from,
                        to,
                        startC,
                        endC,
                        transport,
                        distance,
                        duration,
                        mappath)
                    );
                }
                reader.Close();
                return tours;
            }
        }
    }
}
