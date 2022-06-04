using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static void AddTour(Tour tour)
        {

            //with the using statement we make sure the connection gets freed right after the block is done
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                //open the DB connection and create a command
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                command.CommandText = @"
                    INSERT INTO tours 
                    (t_id, t_name, t_description, t_startname, t_endname, t_startcoord, t_endcoord, t_transporttype, t_distance, t_estimatetime, t_mappath)
                    VALUES (@t_id, @t_name, @t_description, @t_startname, @t_endname, @t_startcoord, @t_endcoord, @t_transporttype, @t_distance, @t_estimatetime, @t_mappath)              
                    ";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("t_id", NpgsqlDbType.Uuid, 50);
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

                c.Parameters["t_id"].Value = tour.ID;
                c.Parameters["t_name"].Value = tour.Name;
                c.Parameters["t_description"].Value = tour.Description;
                c.Parameters["t_startname"].Value = tour.From.ToString();
                c.Parameters["t_endname"].Value = tour.To.ToString();

                c.Parameters["t_startcoord"].Value = 123;
                c.Parameters["t_endcoord"].Value = 321;
                c.Parameters["t_transporttype"].Value = 1;
                c.Parameters["t_distance"].Value = 15;

                c.Parameters["t_estimatetime"].Value = new TimeSpan(2, 23, 5);

                c.Parameters["t_mappath"].Value = "test";

                command.ExecuteNonQuery();
            }

        }

        public static Collection<Tour> getTours(){

            //with the using statement we make sure the connection gets freed right after the block is done
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                //open the DB connection and create a command
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                Collection<Tour> tours = new Collection<Tour>();

                command.CommandText = @"
                    SELECT t_id, t_name, t_description, t_startname, t_endname, t_startcoord, t_endcoord, t_transporttype, t_distance, t_estimatetime, t_mappath
                    FROM tours";
                
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tour tour = new Tour(Guid.Parse(reader[0].ToString()),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        new Adress(),
                                        new Adress(),
                                        reader.GetInt32(5),
                                        reader.GetInt32(6),
                                        reader.GetString(7),
                                        reader.GetInt32(8),
                                        new TimeSpan(2, 23, 5),
                                        reader.GetString(10)
                                        );
                    tours.Add(tour);
                }
                
                return tours;
            }
        }

        public static bool DeleteTour(Guid id)
        {

            //with the using statement we make sure the connection gets freed right after the block is done
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                //open the DB connection and create a command
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                Collection<Tour> tours = new Collection<Tour>();

                command.CommandText = @"
                    DELETE FROM tours WHERE t_id = @id
                ";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("id", NpgsqlDbType.Uuid);

                c.Prepare();

                c.Parameters["id"].Value = id;

                command.ExecuteNonQuery();

                return true;
            }
        }

    }
}
