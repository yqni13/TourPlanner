using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using TourPlanner.ViewModels.SubViewModels;

namespace TourPlanner.UnitTests
{
    public class ViewModelTests
    {
       

        
        [Test]
        public void Test_TourOverviewVM_DefaultImagePathNotNull()
        {
            //arrange  
            TourOverviewViewModel vm = new TourOverviewViewModel();
            //Act
            vm.SetMapPath(null);
            //Assert
            Assert.AreNotEqual(vm.Image, null);
        }
        [Test]
        public void Test_TourOverviewVM_DefaultImagePathCorrect()
        {
            //arrange
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("Config/TourPlanner.json", optional: false, reloadOnChange: true)
                .Build();

            String defaultPath = $"{Environment.CurrentDirectory}/{configuration["images:path"]}/image_placeholder.jpg";

            TourOverviewViewModel vm = new TourOverviewViewModel();
            //Act
            vm.SetMapPath(null);
            //Assert
            Assert.AreEqual(vm.Image, defaultPath);
        }

        //https://stackoverflow.com/questions/49789228/unit-testing-wpf-disable-message-boxes
        //To test this need to implement Message interface
        /*
        [Test]
        public void Test_AddValidation()
        {
            //arrange
            TourLogs log = new();

            AddLogViewModel vm = new AddLogViewModel();

            vm.NewLog = log;
            //Act
            vm.SetMapPath(null);
            //Assert
            Assert.AreEqual(vm.Image, defaultPath);
            
        }
        */
        [Test]
        public void Test_UpdateResultsVM()
        {
            //arrange  
            TourDataResultsViewModel vm = new TourDataResultsViewModel();
            Collection<Tour> tourrcoll = new Collection<Tour>();
            //Act
            vm.UpdateTours(tourrcoll);
            //Assert
            Assert.AreEqual(vm.Data, tourrcoll);
        }


    }
}
