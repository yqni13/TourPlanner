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
using TourPlanner.Models.Enums;

namespace TourPlanner.DAL
{
    public class TourLogAccess
    {        
        public static void AddTourLog(TourLogs log, int diff, int rating)
        {
            
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO tourlogs (tl_id, tl_tour, tl_date, tl_time, tl_difficulty, tl_comment, tl_rating, tl_distance)" +
                    $"VALUES (@tl_id, @tl_tour, @tl_date, @tl_time, @tl_difficulty, @tl_comment, @tl_rating, @tl_distance)";
                NpgsqlCommand c = command as NpgsqlCommand;
                c.Parameters.Add("tl_id", NpgsqlDbType.Uuid, 50);
                c.Parameters.Add("tl_tour", NpgsqlDbType.Uuid, 50);
                c.Parameters.Add("tl_date", NpgsqlDbType.Date, 50);
                c.Parameters.Add("tl_time", NpgsqlDbType.Time, 50);
                c.Parameters.Add("tl_difficulty", NpgsqlDbType.Integer, 50);
                c.Parameters.Add("tl_comment", NpgsqlDbType.Varchar, 500);
                c.Parameters.Add("tl_rating", NpgsqlDbType.Integer, 50);
                c.Parameters.Add("tl_distance", NpgsqlDbType.Double, 50);

                c.Prepare();

                c.Parameters["tl_id"].Value = log.LogID;
                c.Parameters["tl_tour"].Value = log.TourID;
                c.Parameters["tl_date"].Value = log.Timestamp;
                c.Parameters["tl_time"].Value = log.TotalTime;
                c.Parameters["tl_difficulty"].Value = diff;
                c.Parameters["tl_comment"].Value = log.Comment;
                c.Parameters["tl_rating"].Value = rating;
                c.Parameters["tl_distance"].Value = log.Distance;

                command.ExecuteNonQuery();                
            }
        }

        public static Collection<TourLogs> GetAllTourLogs()
        {
            TourLogs log;
            Collection<TourLogs> logs = new();
            string tableName = "tourlogs";
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM {tableName}";

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    log = new(
                        Guid.Parse(reader[0].ToString()),
                        Guid.Parse(reader[1].ToString()),
                        (DateTime)reader.GetValue(2),
                        (TimeSpan)reader.GetValue(3),
                        (ETourDifficulty)reader.GetValue(4),
                        reader.GetString(5),
                        (ETourRating)reader.GetValue(6),
                        reader.GetDouble(7));

                    logs.Add(log);
                }

                return logs;
            }
        }

        public static Collection<TourLogs> GetLogsFromSpecificTour(Guid tourId)
        {
            TourLogs log;
            Collection<TourLogs> logs = new();
            string tableName = "tourlogs";
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                command.CommandText = $"SELECT * FROM {tableName} WHERE tl_tour=@tourFK";

                var tourFK = command.CreateParameter();
                tourFK.DbType = DbType.Guid;
                tourFK.ParameterName = "tourFK";
                tourFK.Value = tourId;
                command.Parameters.Add(tourFK);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    log = new(
                        Guid.Parse(reader[0].ToString()),
                        Guid.Parse(reader[1].ToString()),
                        (DateTime)reader.GetValue(2),
                        (TimeSpan)reader.GetValue(3),
                        (ETourDifficulty)reader.GetValue(4),
                        reader.GetString(5),
                        (ETourRating)reader.GetValue(6),
                        reader.GetDouble(7));

                    logs.Add(log);
                }

                return logs;
            }
        }

        public static void UpdateTourLog(TourLogs log)
        {
            string tableName = "tourlogs";
            using (IDbConnection connection = DBConnection.GetConnection())
            {
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                Collection<Tour> tours = new Collection<Tour>();

                command.CommandText = $"UPDATE {tableName} SET tl_time=@time, tl_difficulty=@diff, tl_comment=@cmnt, tl_rating=@rating, tl_distance=@distance WHERE tl_id = @logId";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("time", NpgsqlDbType.Time, 50);
                c.Parameters.Add("diff", NpgsqlDbType.Integer, 50);
                c.Parameters.Add("cmnt", NpgsqlDbType.Varchar, 50);
                c.Parameters.Add("rating", NpgsqlDbType.Integer, 50);
                c.Parameters.Add("logId", NpgsqlDbType.Integer, 50);
                c.Parameters.Add("distance", NpgsqlDbType.Double, 50);

                c.Prepare();

                c.Parameters["time"].Value = log.TotalTime;
                c.Parameters["diff"].Value = log.Difficulty;
                c.Parameters["cmnt"].Value = log.Comment;
                c.Parameters["rating"].Value = log.Rating;
                c.Parameters["logId"].Value = log.LogID;
                c.Parameters["distance"].Value = log.Distance;

                command.ExecuteNonQuery();

            }
        }

        public static void DeleteTourLog(Guid logID)
        {
            string tableName = "tourlogs";
            using (IDbConnection connection = DBConnection.GetConnection())
            {                
                connection.Open();
                IDbCommand command = connection.CreateCommand();

                Collection<Tour> tours = new Collection<Tour>();

                command.CommandText = $"DELETE FROM {tableName} WHERE tl_id = @id";

                NpgsqlCommand c = command as NpgsqlCommand;

                c.Parameters.Add("id", NpgsqlDbType.Uuid);

                c.Prepare();

                c.Parameters["id"].Value = logID;

                command.ExecuteNonQuery();
            }
        }
    }
}
