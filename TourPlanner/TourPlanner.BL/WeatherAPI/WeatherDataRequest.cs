using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.BL.WeatherAPI
{
    class WeatherDataRequest
    {
        public static Weather getWeather()
        {
            return new Weather();
        }
    }
}
