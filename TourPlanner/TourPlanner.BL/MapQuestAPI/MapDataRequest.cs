using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
                HttpClient client = new();
                var response = client.GetStringAsync(MapDataURL);

                // Wait for the request to complete and return requested data.
                var responseContent = await response;

                // Deserialize the response data to json object for further handling.
                JsonResponse = JsonConvert.DeserializeObject<JObject>(responseContent);

                // Parsing content and filling up Tour-model.
                tour = ParsingResponse(JsonResponse);
                tour.ID = Guid.NewGuid();

                // To-Do: fill rest of data into TourObject

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

        }

        public Tour ParsingResponse(JObject json)
        {
            // Parse BoundingBox, Session, distance and time into TourObject
        }
    }
}
