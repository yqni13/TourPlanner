namespace Example.Log4Net.logging
{
    //Taken from example code SWEN2 lecture.
    public static class LoggerFactory
    {
        public static ILoggerWrapper GetLogger()
        {
            return Log4NetWrapper.CreateLogger("./log4net.config");
        }
    }
}
