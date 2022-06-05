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
    public class TourController
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public static Collection<Tour> GetTours()
        {
            Collection<Tour> tours = new();
            try
            {
                tours = TourAccess.getTours();
                logger.Debug("Fetched " + tours.Count.ToString() + " from Database");
            }
            catch
            {
                logger.Error("Could not get tours from DB");
            }
            return tours;
        }

        public static void AddTour(Tour tour)
        {
            tour.ID = System.Guid.NewGuid();
            try
            {
                TourAccess.AddTour(tour);               
                logger.Debug("Added tour with ID " + tour.ID + " to Database");                
            }

            catch (Exception err)
            {
                logger.Error("Failed to add tour " + tour.ID + " to Database");
                MessageBox.Show(err.Message);
            }            
        }

        public static void DeleteTour(Tour tour)
        {            
            try
            {
                TourAccess.DeleteTour(tour.ID);
                logger.Debug("Deleted tour with ID " + tour.ID + " from Database");                
            }
            catch
            {
                logger.Error("Failed to delete tour " + tour.ID + " from Database");                
            }
        }

        public static void ImportTour(String path)
        {
            Collection<Tour> existingTours = TourAccess.getTours();
            Tour tour = TourIO.ImportTour(path);
            //LINQ statement to check if tour with same id exists
            // https://stackoverflow.com/questions/56508215/how-to-check-if-an-object-with-the-same-id-already-exist-inside-a-list-of-object
            if (existingTours.Any(t => t.ID == tour.ID))
            {
                logger.Error("Tried importing tour, but already existed");
                return;
            }
            try
            {
                
                TourAccess.AddTour(tour);
            }
            catch
            {
                logger.Error("Failed to Import tour");
            }
            
        }

        private Tour getMapQuestData(Tour tour)
        {
            return tour;
        }
    }
}
