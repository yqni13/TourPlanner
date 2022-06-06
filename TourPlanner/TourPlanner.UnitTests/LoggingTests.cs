using Example.Log4Net.logging;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.UnitTests
{
    public class LoggingTests
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();
        
        [OneTimeTearDown]
        public void Teardown()
        {            
            File.Delete("logfile.log");
        }

        [Test, Order(1)]
        public void Test_Logger_Logfilecreated()
        {
            //arrange

            //Act
            logger.Debug("Some Debug Log");
            //Assert
            Assert.True(File.Exists("logfile.log"));            
        }
    }
}
