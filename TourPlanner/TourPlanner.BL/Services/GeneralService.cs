using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.Services
{
    public static class GeneralService
    {
        public static double StringTimeConverterToSeconds(string time)
        {
            DateTime timeType = DateTime.Parse(time);
            double seconds = timeType.Second;
            double minutes = timeType.Minute * 60;
            double hours = timeType.Hour * 3600;
            return seconds + minutes + hours;
        }

        public static Tour AddTourDescription(Tour tour, string description)
        {
            tour.Description = description;
            return tour;
        }

        public static List<string> GetRouteTypes()
        {
            List<string> types = new();
            types.Add("fastest");
            types.Add("shortest");
            types.Add("pedestrian");
            types.Add("bicyle");

            return types;
        }

        public static int CalculatePopularity(Tour tour)
        {
            int result = 0;
            foreach(TourLogs log in tour.TourLogs)
            {
                result += 1;
            }
            return result;
        }

        public static string CalculateChildFriendly(Tour tour)
        {
            return "Childfriendly";
        }

        public static TimeSpan Average(IEnumerable<TimeSpan> spans)
        {
            return TimeSpan.FromSeconds(spans.Select(s => s.TotalSeconds).Average());
        }
    }    
}
