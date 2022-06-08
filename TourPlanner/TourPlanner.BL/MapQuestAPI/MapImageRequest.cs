using Example.Log4Net.logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Models;

namespace TourPlanner.BL.MapQuestAPI
{
    public class MapImageRequest
    {
        public String MapImageURL { get; set; } = "https://www.mapquestapi.com/staticmap/v5/map";
        public String SessionID { get; set; }
        public String ImageDirectoryPath { get; set; }
        public String KeyAuthentication { get; set; }
        public String MapSize { get; set; }
        public String Format { get; set; }
        public String RouteColor { get; set; }
        public String ScaleBar { get; set; }

        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public MapImageRequest()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            ImageDirectoryPath = configuration["images:path"];
            
            MapSize = "600,400";
            Format = "png";
            ScaleBar = "true|bottom";
            KeyAuthentication = configuration["mapquestapi:key"];
        }

        public async Task<Tour> RequestMapImageFromAPI(Tour tour)
        {
            string imageName;

            try
            {
                // Prepare URL with necessary/customized parameters.
                SetupURL(tour.MapType, tour.Session, tour.BoundingBox);

                using HttpClient client = new();

                // Wait for the request to complete and return requested image by reading the bytes.
                var response = await client.GetByteArrayAsync(MapImageURL);

                var reg = new Regex(@"[^\-\""'()*+,./0-9<=>@A-Z\[\\\]^_`a-z{|}]");

                string tourName = reg.Replace(tour.Name, "_");

                // Generate file name by Guid (avoid DateTime format drama with no ":") and name of tour.
                imageName = $"{tour.ID}_{tourName}.{Format}";
                tour.MapPath = $"{Environment.CurrentDirectory}/{ImageDirectoryPath}/{imageName}";
                if(!File.Exists(tour.MapPath))
                {
                    // Draw Image with data from response and call MapPath from tour model.
                    using System.Drawing.Image mapImage = System.Drawing.Image.FromStream(new MemoryStream(response));
                    mapImage.Save(tour.MapPath, ImageFormat.Png);
                }
            }
            catch (NullReferenceException err)
            {
                logger.Error(err.ToString());
                throw;
            }
            catch (Exception err)
            {
                logger.Error(err.ToString());
                throw;
            }
            return tour;
        }

        public void SetupURL(string maptype, string session, string boundingbox)
        {
            if (maptype == "light" || maptype == "map")
                RouteColor = "A64DFF";
            else
                RouteColor = "FFFF33";

            MapImageURL += $"?key={KeyAuthentication}&size={MapSize}&zoom={7}&session={session}" +
                $"&boundingBox={boundingbox}&format={Format}&type={maptype}&scalebar={ScaleBar}&routeColor={RouteColor}";
        }
    }
}
