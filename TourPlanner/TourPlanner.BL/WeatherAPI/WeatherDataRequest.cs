using Example.Log4Net.logging;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourPlanner.Models;

namespace TourPlanner.BL.WeatherAPI
{
    public class WeatherDataRequest
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();
        public static Weather getWeather(Tour tour)
        {
            Weather weather = new();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            try
            {
                string[] substrings = tour.BoundingBox.Split(',', StringSplitOptions.RemoveEmptyEntries);
                string location = $"{substrings[0]}, {substrings[1]}";
                string key = configuration["weatherapi:key"];

                String requestURL = "http://api.weatherapi.com/v1/current.json?q=" + location +"&key=" + key;

                using HttpClient client = new();

                // Wait for the request to complete and return requested data.
                Task<String> requesttask = Task.Run<String>(async () => await client.GetStringAsync(requestURL));
                var response = requesttask.Result;

                // Deserialize the response data to json object for further handling.
                JObject JsonResponse = JsonConvert.DeserializeObject<JObject>(response);

                // Parsing content and filling up Tour-model.
                weather.Temp = JsonResponse["current"]["temp_c"].ToString() + " C°";
                weather.FeltTemp = JsonResponse["current"]["feelslike_c"].ToString() + " C°";
                weather.WeatherCondition = JsonResponse["current"]["condition"]["text"].ToString();
            }
            catch (NullReferenceException err)
            {
                MessageBox.Show(err.ToString());
                logger.Error("Null ref at weather data rewuest");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString());
                logger.Error("fatal error at weather request");
            }
            return weather;
        }
    }
}
