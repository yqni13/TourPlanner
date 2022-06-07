using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                    (t_id, t_name, t_description, t_startname, t_endname, t_transporttype, t_distance, t_estimatetime, t_mappath)
                    VALUES (@t_id, @t_name, @t_description, @t_startname, @t_endname, @t_transporttype, @t_distance, @t_estimatetime, @t_mappath)              
                    ";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("t_id", NpgsqlDbType.Uuid, 50);
                c.Parameters.Add("t_name", NpgsqlDbType.Varchar, 50);
                c.Parameters.Add("t_description", NpgsqlDbType.Varchar, 500);
                c.Parameters.Add("t_startname", NpgsqlDbType.Varchar, 50);
                c.Parameters.Add("t_endname", NpgsqlDbType.Varchar, 50);

                c.Parameters.Add("t_transporttype", NpgsqlDbType.Varchar, 50);
                c.Parameters.Add("t_distance", NpgsqlDbType.Double, 50);

                c.Parameters.Add("t_estimatetime", NpgsqlDbType.Time, 50);

                c.Parameters.Add("t_mappath", NpgsqlDbType.Varchar, 400);

                c.Prepare();

                c.Parameters["t_id"].Value = tour.ID;
                c.Parameters["t_name"].Value = tour.Name;
                c.Parameters["t_description"].Value = tour.Description;
                c.Parameters["t_startname"].Value = tour.From.ToString();
                c.Parameters["t_endname"].Value = tour.To.ToString();
               
                c.Parameters["t_transporttype"].Value = tour.Transport;
                c.Parameters["t_distance"].Value = tour.Distance;

                c.Parameters["t_estimatetime"].Value = tour.Duration;

                c.Parameters["t_mappath"].Value = tour.MapPath;

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
                    SELECT t_id, t_name, t_description, t_startname, t_endname, t_transporttype, t_distance, t_estimatetime, t_mappath
                    FROM tours";
                
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Tour tour = new Tour(Guid.Parse(reader[0].ToString()),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        new Adress(reader.GetString(3)),
                                        new Adress(reader.GetString(4)),
                                        reader.GetString(5),
                                        reader.GetInt32(6),                                        
                                        (TimeSpan)reader.GetValue(7),                                        
                                        reader.GetString(8)
                                        ); ;
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

        public static void UpdateTour(Tour tour)
        {            
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                Collection<Tour> tours = new Collection<Tour>();

                command.CommandText = $"UPDATE tours SET t_name=@name, t_description=@description WHERE t_id=@id";

                NpgsqlCommand c = command as NpgsqlCommand;
                                
                c.Parameters.Add("name", NpgsqlDbType.Varchar, 50);
                c.Parameters.Add("description", NpgsqlDbType.Varchar, 500);
                c.Parameters.Add("id", NpgsqlDbType.Uuid);

                c.Prepare();

                c.Parameters["name"].Value = tour.Name;
                c.Parameters["description"].Value = tour.Description;
                c.Parameters["id"].Value = tour.ID;                

                command.ExecuteNonQuery();
            }
        }

    }
}
