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
                TourLogAccess.AddTourLog(log);
                logger.Debug("Added TourLog with ID " + log.LogID + " to Database");
            }
            catch (Exception err)
            {
                logger.Error("Failed to add tour " + log.LogID + " to Database");
                MessageBox.Show(err.Message);
            }
        }

        public static void DeleteTourLog(TourLogs log)
        {
            try
            {
                TourLogAccess.DeleteTourLog(log.LogID);
                logger.Debug("Deleted tour with ID " + log.LogID + " from Database");
            }
            catch (Exception err)
            {
                logger.Error("Failed to delete tour " + log.LogID + " from Database");
                MessageBox.Show(err.Message);
            }
        }

        public static void UpdateTourLog(TourLogs log)
        {
            try
            {
                TourLogAccess.UpdateTourLog(log);
                logger.Debug("Updated tour with ID " + log.LogID + " in Database");
            }
            catch (Exception err)
            {
                logger.Error("Failed to update tour " + log.LogID + " in Database");
                MessageBox.Show(err.Message);
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
            catch (Exception err)
            {
                logger.Error("Could not get logs from DB");
                MessageBox.Show(err.Message);
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
            catch (Exception err)
            {
                logger.Error("Could not get logs from DB");
                MessageBox.Show(err.Message);
            }
            return logs;
        }
    }
}
