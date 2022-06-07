namespace Example.Log4Net.logging
{
    public interface ILoggerWrapper
    {
        //Taken from example code SWEN2 lecture.
        void Debug(string message);
        void Error(string message);
        void Fatal(string message);
        void Warn(string message);
    }
}