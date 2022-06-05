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


        public MapDataRequest(Tour tour)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            this.TourObject = tour;
            //need name, from, to and transport type in TourObject to work 
            //maybe make ifstatement            

            // Get saved authentication key (http://developer.mapquest.com) from config file.
            KeyAuthentication = configuration["mapquestapi:key"];

            // Connect request link with necessary parameters: key | from | to | unit | routeType.
            MapDataURL += $"?key={KeyAuthentication}&from={TourObject.From.ToString()}&to={TourObject.To.ToString()}&unit=k&routeType={TourObject.Transport}";
           
        }

        public async Task<Tour> RequestTourFromAPI()
        {            
            try
            {                
                using HttpClient client = new();

                // Wait for the request to complete and return requested data.
                var response = await client.GetStringAsync(MapDataURL);
                MessageBox.Show(response);
                
                // Deserialize the response data to json object for further handling.
                JsonResponse = JsonConvert.DeserializeObject<JObject>(response);

                // Parsing content and filling up Tour-model.

                ParsingResponse(JsonResponse);                
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
            return TourObject;
        }

        public void ParsingResponse(JObject json)
        {
            // Parse BoundingBox, Session, distance and time into TourObject

            string sessionID = json["route"]["sessionId"].ToString();
            string boundingBox_lr_lng = json["route"]["boundingBox"]["lr"]["lng"].ToString().Replace(",", ".");
            string boundingBox_lr_lat = json["route"]["boundingBox"]["lr"]["lat"].ToString().Replace(",", ".");
            string boundingBox_ul_lng = json["route"]["boundingBox"]["ul"]["lng"].ToString().Replace(",", ".");
            string boundingBox_ul_lat = json["route"]["boundingBox"]["ul"]["lat"].ToString().Replace(",", ".");
            string boundingBox = $"{boundingBox_lr_lat},{boundingBox_lr_lng},{boundingBox_ul_lat},{boundingBox_ul_lng}";
            string distance = json["route"]["distance"].ToString();
            string time = json["route"]["formattedTime"].ToString();            
            TimeSpan tourTime = TimeSpan.FromSeconds(GeneralService.StringTimeConverterToSeconds(time));

            MessageBox.Show(sessionID);

            TourObject.Session = sessionID;
            TourObject.BoundingBox = boundingBox;
            TourObject.Distance = Double.Parse(distance);
            TourObject.Duration = tourTime;
        }
    }
    
}
