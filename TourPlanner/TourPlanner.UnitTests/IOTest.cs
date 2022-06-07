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
            tour.MapPath = null;
        }
        [OneTimeTearDown]
        public void Teardown()
        {
            foreach (System.IO.FileInfo file in new DirectoryInfo("TestIO").GetFiles()) file.Delete();
            Directory.Delete("TestIO");
        }
        
        [Test]
        public void Test_ExportTour_ThrowsDirectoryNotFoundException()
        {            
            //Assert
            Assert.Throws<DirectoryNotFoundException>(() => TourIO.ExportTour(tour, "WrongPath/testtour.json"));            
        }

        [Test, Order(1)]
        public void Test_ExportTour_FileCreated_True()
        {
            //arrange

            //Act
            TourIO.ExportTour(tour, "TestIO/testtour.json");
            //Assert
            Assert.True(File.Exists("TestIO/testtour.json"));
        }
        [Test, Order(2)]
        public void Test_ImportTour_SameName()
        {
            //arrange

            //Act
            Tour imported = TourIO.ImportTour("TestIO/testtour.json");
            //Assert
            Assert.AreEqual(imported.Name, tour.Name);
        }
        [Test, Order(3)]
        public void Test_ExportTour_NullValueOmitted()
        {
            //arrange

            //Act
            Tour imported = TourIO.ImportTour("TestIO/testtour.json");
            //Assert
            Assert.True(imported.MapPath == String.Empty);
        }
        [Test]
        public void Test_ImportTour_ThrowsDirectoryNotFoundException()
        {
            //Assert
            Assert.Throws<DirectoryNotFoundException>(() => TourIO.ImportTour("WrongPath/testtour.json"));
        }
    }
}
