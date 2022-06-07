using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.WeatherAPI;
using TourPlanner.Models;

namespace TourPlanner.UnitTests
{
    public class TestWeatherAPI
    {
        [Test]
        public void Test_WeatherAPI_getValidWeather_Temperatur()
        {
            //arrange
            Tour tour = new Tour();
            tour.BoundingBox = "48.001,16.44,48.343,16.98";
            //Act
            Weather weather = WeatherDataRequest.getWeather(tour);
            //Assert
            Assert.AreNotEqual(weather.Temp, String.Empty);
        }
        [Test]
        public void Test_WeatherAPI_getValidWeather_Description()
        {
            //arrange
            Tour tour = new Tour();
            tour.BoundingBox = "48.001,16.44,48.343,16.98";
            //Act
            Weather weather = WeatherDataRequest.getWeather(tour);
            //Assert
            Assert.AreNotEqual(weather.WeatherCondition, String.Empty);
        }
        [Test]
        public void Test_WeatherAPI_InvalidCoord()
        {
            //arrange
            Tour tour = new Tour();
            tour.BoundingBox = "";
            //Act
            
            //Assert
            Assert.Throws<IndexOutOfRangeException>(() => WeatherDataRequest.getWeather(tour));            
        }
    }
}
