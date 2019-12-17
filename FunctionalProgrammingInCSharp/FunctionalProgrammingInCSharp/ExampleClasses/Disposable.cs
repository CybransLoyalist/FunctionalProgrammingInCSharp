using System;

namespace FunctionalProgrammingInCSharp.ExampleClasses
{
    public interface ILogger
    {
        void Log(string message);
    }

    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    public class Disposable : IDisposable
    {
        private readonly ILogger _logger;

        public Disposable(ILogger logger)
        {
            _logger = logger;
        }

        public string DoBeforeDisposing()
        {
            return "I'm doing something useful.";
        }

        public void Dispose()
        {
            _logger.Log("I'm disposing myself. Good bye.");
        }
    }
}
