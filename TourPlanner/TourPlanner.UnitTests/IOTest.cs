using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BL.Services;
using TourPlanner.Models;

namespace TourPlanner.UnitTests
{
    public class IOTest
    {
        public Tour tour;

        [OneTimeSetUp]
        public void Setup()
        {
            tour = new Tour(
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
            Directory.CreateDirectory("TestIO");
        }
        [OneTimeTearDown]
        public void Teardown()
        {
            foreach (System.IO.FileInfo file in new DirectoryInfo("TestIO").GetFiles()) file.Delete();
            //Directory.Delete("TestIO");
        }

        [Test]
        public void Test_ExportTour_FileCreated_True()
        {
            //arrange

            //Act
            TourIO.ExportTour(tour, "TestI/testtour.json");
            //Assert
            Assert.True(File.Exists("TestIO/testtour.json"));
        }
    }
}
