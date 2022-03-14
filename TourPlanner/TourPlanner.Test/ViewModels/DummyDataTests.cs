using System;
using NUnit.Framework;
using TourPlanner.UI.ViewModels;
using TourPlanner.UI.TourSearch;

namespace TourPlanner.Test.ViewModels
{
    [TestFixture]
    public class DummyDataTests
    {
        private TourOverviewViewModel _tourOverviewViewModel;
        private SearchTourData _searchTourData;

        [SetUp]
        public void Setup()
        {
            _tourOverviewViewModel = new();
            _searchTourData = new();
        }

        [Test]
        public void TestBinding_ShouldNotContainDataBeforeClickEvent()
        {
            Assert.IsTrue(_tourOverviewViewModel.IsResultEmpty(), "Test data does contain content before explicit binding event.");
        }

        [Test]
        public void TestBinding_ShouldContainMultipleContent()
        {
            // For now we test the purpose of returning an array if it returns only a single string.
            Assert.IsTrue((_searchTourData.TourSearch("").Length > 1) ? true : false, "Test array does contain 1 or no string as content.");
        }
    }
}
