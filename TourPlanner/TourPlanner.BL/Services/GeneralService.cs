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
    }    
}
