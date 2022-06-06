using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Search;
using TourPlanner.Models;

namespace TourPlanner.UnitTests
{
    public class SearEngineTests
    {
        Collection<Tour> tours = new Collection<Tour>();
        [OneTimeSetUp]
        public void Setup()        {
            
            Tour tour1 = new Tour(
                Guid.NewGuid(),
                "TestTour",
                "Test Description",
                new Adress(),
                new Adress(),
                "Bike",
                200,
                new TimeSpan(2, 12, 13),
                "//Somepath/test"
            );
            tours.Add(tour1);
            Tour tour2 = new Tour(
                Guid.NewGuid(),
                "ABC Tour",
                "ABC Description",
                new Adress(),
                new Adress(),
                "Bike",
                200,
                new TimeSpan(2, 12, 13),
                "//Somepath/test"
            );
            tours.Add(tour2);
            Tour tour3 = new Tour(
                Guid.NewGuid(),
                "New Tour",
                "Test Description",
                new Adress(),
                new Adress(),
                "Bike",
                200,
                new TimeSpan(2, 12, 13),
                "//Somepath/test"
            );
            tours.Add(tour3);
            Tour tour4 = new Tour(
                Guid.NewGuid(),
                "Hard to find",
                "Test Description",
                new Adress(),
                new Adress(),
                "Bike",
                200,
                new TimeSpan(2, 12, 13),
                "//Somepath/test"
            );
            tours.Add(tour4);
        }
        [Test]
        public void Test_Searchengine_FindTourFullName()
        {
            //arrange  

            //Act
            Collection<Tour> result = SearchEngine.searchTours(tours, "Hard to find");
            //Assert
            Assert.AreEqual(result.Count, 1);
        }
        [Test]
        public void Test_Searchengine_FindMultible()
        {
            //arrange  

            //Act
            Collection<Tour> result = SearchEngine.searchTours(tours, "Tour");
            //Assert
            Assert.AreEqual(result.Count, 3);
        }
        [Test]
        public void Test_Searchengine_NoResults()
        {
            //arrange  

            //Act
            Collection<Tour> result = SearchEngine.searchTours(tours, "thisstringdoesntexist");
            //Assert
            Assert.AreEqual(result.Count, 0);
        }
        [Test]
        public void Test_Searchengine_FindAllemptyStrings()
        {
            //arrange  

            //Act
            Collection<Tour> result = SearchEngine.searchTours(tours, "");
            //Assert
            Assert.AreEqual(result.Count, 4);
        }

    }
}
