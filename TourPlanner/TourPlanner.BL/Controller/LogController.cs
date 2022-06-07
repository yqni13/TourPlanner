using Example.Log4Net.logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.DAL;
using TourPlanner.Models;
using TourPlanner.Models.Enums;

namespace TourPlanner.BL.Services
{
    public class LogController
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public static void AddTourLog(TourLogs log)
        {
            if(log.LogID == Guid.Empty)
                log.LogID = Guid.NewGuid();
                        
            try
            {
                int difficulty = GetETourDifficultyInt(log.Difficulty);
                int rating = GetETourRatingInt(log.Rating);

                TourLogAccess.AddTourLog(log, difficulty, rating);
                logger.Debug("Added TourLog with ID " + log.LogID + " to Database");
            }
            catch (Exception)
            {
                logger.Error("Failed to add tour " + log.LogID + " to Database");
                throw;
            }
        }

        public static void DeleteTourLog(TourLogs log)
        {
            try
            {
                TourLogAccess.DeleteTourLog(log.LogID);
                logger.Debug("Deleted tour with ID " + log.LogID + " from Database");
            }
            catch (Exception)
            {
                logger.Error("Failed to delete tour " + log.LogID + " from Database");
                throw;
            }
        }

        public static void UpdateTourLog(TourLogs log)
        {
            try
            {
                TourLogAccess.UpdateTourLog(log);
                logger.Debug("Updated tour with ID " + log.LogID + " in Database");
            }
            catch (Exception)
            {
                logger.Error("Failed to update tour " + log.LogID + " in Database");
                throw;
            }
        }

        public static Collection<TourLogs> GetAllLogs()
        {
            Collection<TourLogs> logs = new();
            try
            {
                logs = TourLogAccess.GetAllTourLogs();
                logger.Debug("Fetched all " + logs.Count.ToString() + " logs from Database");
            }
            catch (Exception)
            {
                logger.Error("Could not get logs from DB");
                throw;
            }
            return logs;
        }

        public static Collection<TourLogs> GetSpecificLogs(Guid tourId)
        {
            Collection<TourLogs> logs = new();
            try
            {
                logs = TourLogAccess.GetLogsFromSpecificTour(tourId);
                logger.Debug("Fetched specific " + logs.Count.ToString() + " logs from Database");
            }
            catch (Exception)
            {
                logger.Error("Could not get logs from DB");
                throw;
            }
            return logs;
        }

        public static ETourDifficulty GetETourDifficultyEnumeration(string selection)
        {
            if (selection == ETourDifficulty.Warmup.GetDescription())
                return (ETourDifficulty)1;
            else if (selection == ETourDifficulty.Easy.GetDescription())
                return (ETourDifficulty)2;
            else if (selection == ETourDifficulty.Moderate.GetDescription())
                return (ETourDifficulty)3;
            else if (selection == ETourDifficulty.Hard.GetDescription())
                return (ETourDifficulty)4;
            else if (selection == ETourDifficulty.Expert.GetDescription())
                return (ETourDifficulty)5;

            return (ETourDifficulty)1;
        }

        public static ETourRating GetETourRatingEnumeration(string selection)
        {
            if (selection == ETourRating.Worst.GetDescription())
                return (ETourRating)1;
            else if (selection == ETourRating.Bad.GetDescription())
                return (ETourRating)2;
            else if (selection == ETourRating.Weak.GetDescription())
                return (ETourRating)3;
            else if (selection == ETourRating.Improveable.GetDescription())
                return (ETourRating)4;
            else if (selection == ETourRating.Moderate.GetDescription())
                return (ETourRating)5;
            else if (selection == ETourRating.Advancement.GetDescription())
                return (ETourRating)6;
            else if (selection == ETourRating.Good.GetDescription())
                return (ETourRating)7;
            else if (selection == ETourRating.Excellent.GetDescription())
                return (ETourRating)8;
            else if (selection == ETourRating.Satisfying.GetDescription())
                return (ETourRating)9;
            else if (selection == ETourRating.Perfect.GetDescription())
                return (ETourRating)10;

            return (ETourRating)1;
        }

        public static int GetETourDifficultyInt(ETourDifficulty selection)
        {
            if (selection == ETourDifficulty.Warmup)
                return 1;
            else if (selection == ETourDifficulty.Easy)
                return 2;
            else if (selection == ETourDifficulty.Moderate)
                return 3;
            else if (selection == ETourDifficulty.Hard)
                return 4;
            else if (selection == ETourDifficulty.Expert)
                return 5;

            return 1;
        }

        public static int GetETourRatingInt(ETourRating selection)
        {
            if (selection == ETourRating.Worst)
                return 1;
            else if (selection == ETourRating.Bad)
                return 2;
            else if (selection == ETourRating.Weak)
                return 3;
            else if (selection == ETourRating.Improveable)
                return 4;
            else if (selection == ETourRating.Moderate)
                return 5;
            else if (selection == ETourRating.Advancement)
                return 6;
            else if (selection == ETourRating.Good)
                return 7;
            else if (selection == ETourRating.Excellent)
                return 8;
            else if (selection == ETourRating.Satisfying)
                return 9;
            else if (selection == ETourRating.Perfect)
                return 10;

            return 1;
        }
    }
}
