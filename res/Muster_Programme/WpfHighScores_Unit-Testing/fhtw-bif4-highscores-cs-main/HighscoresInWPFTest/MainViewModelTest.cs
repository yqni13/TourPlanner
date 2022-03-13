using HighscoresInWPF;
using NUnit.Framework;

namespace HighscoresInWPFTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestData_ShouldContainInitialList()
        {
            // Arrange
            MainViewModel mainViewModel = new MainViewModel();
            // Act
            int expected = 2;
            int actual = mainViewModel.Data.Count;
            // Assert
            Assert.AreEqual(expected, actual, "List should contain two highscores!");
        }

        [Test]
        public void TestAddCommand_ShouldAddHighscore()
        {
            // Arrange
            MainViewModel mainViewModel = new MainViewModel();
            var lastDataCount = mainViewModel.Data.Count;
            const string expectedName = "Susi Sorglos";
            mainViewModel.CurrentUsername = expectedName;
            const string expectedPoints = "100";
            mainViewModel.CurrentPoints = expectedPoints;
            // Act
            mainViewModel.AddCommand.Execute(null); // simulate button click
            var expectedDataCount = lastDataCount + 1;
            var currentDataCount = mainViewModel.Data.Count;
            Assert.AreEqual(expectedDataCount, currentDataCount, "A new item should be added!");
            string currentLastName = mainViewModel.Data[expectedDataCount - 1].Username;
            string currentLastPoints = mainViewModel.Data[expectedDataCount - 1].Points;
            Assert.AreEqual(expectedName, currentLastName, $"The name should be {expectedName}");
            Assert.AreEqual(expectedPoints, currentLastPoints, $"The points should be {expectedPoints}");
        }
    }
}