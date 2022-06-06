using Example.Log4Net.logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.Services
{
    public class TourIO
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        private static readonly JsonSerializerSettings _options
        = new() { NullValueHandling = NullValueHandling.Ignore };

        public static void ExportTour(Tour tour, String path)
        {
            var jsonString = JsonConvert.SerializeObject(tour, _options);
            try
            {
                File.WriteAllText(path, jsonString);
                logger.Debug("Exportet to: " + path);
            }
            catch (ArgumentNullException)
            {
                logger.Error("Export file path cant be null");
                throw;
            }
            catch (PathTooLongException)
            {
                logger.Error("Export file path to long");
                throw;
            }
            catch (DirectoryNotFoundException)
            {
                logger.Error("Directtory not found");
                throw;
            }
            catch
            {
                logger.Error("Exporting File Error");
                throw;
            }
        }

        public static Tour ImportTour(String path)
        {
            Tour tour = new();
            
            try
            {
                string json = File.ReadAllText(path);
                tour = JsonConvert.DeserializeObject<Tour>(json, _options);
                logger.Debug("Importet from " + path + " the tour " + tour.Name);
            }
            catch (JsonSerializationException)
            {
                logger.Error("Serialization Error when importing Tour");
            }
            return tour;
        }
    }
}
