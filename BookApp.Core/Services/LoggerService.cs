using BookApp.Core.Interfaces;
using System;
using System.IO;
using System.Threading;

namespace BookApp.Core.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logDirectory;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);


        public LoggerService()
        {
            _logDirectory = Path.Combine(AppContext.BaseDirectory, "Logs");
            if (!Directory.Exists(_logDirectory))
            {
                Directory.CreateDirectory(_logDirectory);
            }
        }


        public void LogInfo(string message)
        {
            var logMessage = $"INFO: {message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            Console.WriteLine(logMessage);
            WriteLogToFile("info.log", logMessage);
        }


        public void LogWarning(string message)
        {
            var logMessage = $"WARNING: {message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            Console.WriteLine(logMessage);
            WriteLogToFile("warning.log", logMessage);
        }


        public void LogError(string message, Exception ex)
        {
            var logMessage = $"ERROR: {message}. Exception: {ex.Message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            Console.WriteLine(logMessage);
            WriteLogToFile("error.log", logMessage);
        }


        private void WriteLogToFile(string fileName, string message)
        {
            var filePath = Path.Combine(_logDirectory, fileName);

            _semaphore.Wait();
            try
            {
                File.AppendAllText(filePath, message + Environment.NewLine);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
