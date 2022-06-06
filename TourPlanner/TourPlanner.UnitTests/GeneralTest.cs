using NUnit.Framework;
using System;
using TourPlanner.BL.Services;
using TourPlanner.Models;
using TourPlanner.Models.Enums;

namespace TourPlanner.UnitTests
{
    public class GeneralTests
    {
        Tour tour;

        [SetUp]
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
        }

        [Test]
        public void Test_StringToSeconds()
        {
            //arrange
            double seconds = 60;
            string timeasstring = "00:01:00";
            //Act
            double toseconds = GeneralController.StringTimeConverterToSeconds(timeasstring);
            //Assert
            Assert.AreEqual(seconds, toseconds);
        }
        [Test]
        public void Test_StringToSeconds_Wrong()
        {
            //arrange
            double seconds = 12;
            string timeasstring = "00:00:14";
            //Act
            double toseconds = GeneralController.StringTimeConverterToSeconds(timeasstring);
            //Assert
            Assert.AreNotEqual(seconds, toseconds);
        }
        [Test]
        public void Test_AverageDiff_SameRating()
        {
            //arrange
            TourLogs log1 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(),
                ETourDifficulty.Easy,
                "Test Description",
                ETourRating.Satisfying,
                34
                );
            tour.TourLogs.Add(log1);

            TourLogs log2 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(),
                ETourDifficulty.Easy,
                "Test Description",
                ETourRating.Satisfying,
                34
                );
            tour.TourLogs.Add(log2);
            //Act
            double difficulty = GeneralController.AverageDifficulty(tour.TourLogs);
            //Assert
            Assert.AreEqual(difficulty, 2);
        }
        [Test]
        public void Test_AverageDiff_DifferentRating()
        {
            //arrange
            TourLogs log1 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(),
                ETourDifficulty.Easy,
                "Test Description",
                ETourRating.Satisfying,
                34
                );
            tour.TourLogs.Add(log1);

            TourLogs log2 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(),
                ETourDifficulty.Hard,
                "Test Description",
                ETourRating.Satisfying,
                34
                );
            tour.TourLogs.Add(log2);
            //Act
            double difficulty = GeneralController.AverageDifficulty(tour.TourLogs);
            //Assert
            Assert.AreEqual(difficulty, 3);
        }

        [Test]
        public void Test_Childfriend_Yes()
        {
            //arrange
            TourLogs log1 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(0,0,50),
                ETourDifficulty.Warmup,
                "Test Description",
                ETourRating.Satisfying,
                12
                );
            tour.TourLogs.Add(log1);

            TourLogs log2 = new TourLogs(
                Guid.NewGuid(),
                Guid.NewGuid(),
                new DateTime(),
                new TimeSpan(0,0,20),
                ETourDifficulty.Easy,
                "Test Description",
                ETourRating.Satisfying,
                1
                );
            tour.TourLogs.Add(log2);
            //Act
            
            //Assert
            Assert.IsTrue(GeneralController.CalculateChildFriendly(tour));
        }
    }
}