using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DAL;
using TourPlanner.Models;

namespace TourPlanner_T_UnitTests.Test.Tests_DB
{
    public class DataBaseTesting
    {
        //private Tour _tours1;
        private List<Tour> _tourList;

        [SetUp]
        public void Setup()
        {
            // Setup new model for tours.
            _tourList = new();
        }

        [Test]
        public void Test_ConnectionToDBWillSucceed()
        {
            Collection<Tour> _tourList = TourAccess.getTours();
            Assert.IsNotNull(_tourList.Count);
        }
    }
}
