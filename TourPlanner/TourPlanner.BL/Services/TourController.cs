using Example.Log4Net.logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.MapQuestAPI;
using TourPlanner.BL.WeatherAPI;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner.BL.Services
{
    public class TourController
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public static object Giud { get; private set; }

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

        public static async void AddTour(Tour tour)
        {
            if(tour.ID == Guid.Empty)
            {
                tour.ID = System.Guid.NewGuid();
            }            
            try
            {
                MapDataRequest datarequest = new(tour);
                MapImageRequest imagerequest = new();

                //https://stackoverflow.com/questions/9343594/how-to-call-asynchronous-method-from-synchronous-method-in-c
                Task<Tour> requesttask = Task.Run<Tour>(async () => await datarequest.RequestTourFromAPI()); 
                Task<Tour> imagetask = Task.Run<Tour>(async () => await imagerequest.RequestMapImageFromAPI(requesttask.Result));
                
                TourAccess.AddTour(imagetask.Result);               
                logger.Debug("Added tour with ID " + tour.ID + " to Database");                
            }

            catch (Exception err)
            {
                logger.Error("Failed to add tour " + tour.ID + " to Database");
                MessageBox.Show(err.ToString());
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
                AddTour(tour);
            }
            catch
            {
                logger.Error("Failed to Import tour");
                throw;
            }
            
        }

        private Tour getMapQuestData(Tour tour)
        {
            return tour;
        }

        public static Weather GetWeather(Tour tour)
        {
            Weather weather = new();
            try
            {
                MapDataRequest datarequest = new(tour);
                
                //https://stackoverflow.com/questions/9343594/how-to-call-asynchronous-method-from-synchronous-method-in-c
                Task<Tour> requesttask = Task.Run<Tour>(async () => await datarequest.RequestTourFromAPI());
                weather = WeatherDataRequest.getWeather(requesttask.Result);

                logger.Debug("Fetched Weather for tour " + tour.ID);
            }

            catch (Exception err)
            {
                logger.Error("Failed to add tour " + tour.ID + " to Database");
                MessageBox.Show(err.ToString());
            }
            //MessageBox.Show(weather.Temp);
            return weather;
        }

        public static void UpdateTour(Tour tour)
        {
            try
            {
                TourAccess.UpdateTour(tour);
            }
            catch (Exception err)
            {
                logger.Error("Failed to update Tour, message: " + err.Message);
                MessageBox.Show(err.ToString());
            }
        }

        public static void DeleteMapFile(string mappath)
        {
            try
            {
                File.Delete(mappath);

            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
            }
        }
    }
}
