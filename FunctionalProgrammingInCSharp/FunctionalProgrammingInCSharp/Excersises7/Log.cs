using System;

namespace FunctionalProgrammingInCSharp.Excersises7
{
    public enum Level { Info, Debug, Error}
    public delegate void Log(Level level, string message);

    public static class LogExtensions
    {
        public static Log CreateLogger(Action<string> logging) => (level, message) => logging($"message: {message} with severity: {level}");
        public static void Info(this Log log, string message)
        {
            log(Level.Info, message);
        }
        public static void Debug(this Log log, string message)
        {
            log(Level.Debug, message);
        }
        public static void Error(this Log log, string message)
        {
            log(Level.Error, message);
        }
    }
}
