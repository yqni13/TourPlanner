using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.BL.Services;
using TourPlanner.Models;

namespace TourPlanner.BL.MapQuestAPI
{
    public class MapDataRequest
    {
        public String MapDataURL { get; set; } = "http://www.mapquestapi.com/directions/v2/route";
        public String KeyAuthentication { get; set; }
        public JObject JsonResponse { get; set; }
        public Tour TourObject { get; set; }
        


        public MapDataRequest(string name, string from, string to, string routeType)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json.config", optional: false, reloadOnChange: true)
                .Build();

            TourObject = new();
            TourObject.Name = name;
            Adress fromAdress = new(from);
            from = fromAdress.ToString();
            Adress toAdress = new(to);
            to = toAdress.ToString();
            TourObject.From = from;
            TourObject.To = to;
            TourObject.Transport = routeType;

            // Get saved authentication key (http://developer.mapquest.com) from config file.
            KeyAuthentication = configuration["mapquestapi:key"];

            // Connect request link with necessary parameters: key | from | to | unit | routeType.
            MapDataURL += $"?key={KeyAuthentication}&from={TourObject.From}&to={TourObject.To}&unit=k&routeType={TourObject.Transport}";
        }

        public async Task<Tour> RequestTourFromAPI()
        {
            Tour tour = new();
            try
            {                
                using HttpClient client = new();

                // Wait for the request to complete and return requested data.
                var response = await client.GetStringAsync(MapDataURL);                

                // Deserialize the response data to json object for further handling.
                JsonResponse = JsonConvert.DeserializeObject<JObject>(response);

                // Parsing content and filling up Tour-model.
                tour = ParsingResponse(JsonResponse);
                tour.ID = Guid.NewGuid();
                // tour.Description = description?
                
            }
            catch (NullReferenceException err)
            {
                MessageBox.Show(err.ToString());
                // Place logger here.
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                // Place logger here.
            }

            return tour;
        }

        public Tour ParsingResponse(JObject json)
        {
            // Parse BoundingBox, Session, distance and time into TourObject

            string sessionID = json["route:sessionId"].ToString();
            string boundingBox_lr_lng = json["route"]["boundingBox"]["lr"]["lng"].ToString().Replace(",", ".");
            string boundingBox_lr_lat = json["route"]["boundingBox"]["lr"]["lat"].ToString().Replace(",", ".");
            string boundingBox_ul_lng = json["route"]["boundingBox"]["ul"]["lng"].ToString().Replace(",", ".");
            string boundingBox_ul_lat = json["route"]["boundingBox"]["ul"]["lat"].ToString().Replace(",", ".");
            string boundingBox = $"{boundingBox_lr_lng},{boundingBox_lr_lat},{boundingBox_ul_lng},{boundingBox_ul_lat}";
            string distance = json["route:distance"].ToString();
            string time = json["route:formattedTime"].ToString();            
            TimeSpan tourTime = TimeSpan.FromSeconds(GeneralService.StringTimeConverterToSeconds(time));

            Tour tour = new();
            tour.Session = sessionID;
            tour.BoundingBox = boundingBox;
            tour.Distance = Double.Parse(distance, CultureInfo.InvariantCulture);
            tour.Duration = tourTime;

            return tour;
        }
    }
}
