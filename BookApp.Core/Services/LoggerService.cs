using BookApp.Core.Interfaces;

namespace BookApp.Core.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly string _logDirectory = "C:/WebApi/BookApp/BookApp.Core/Logs/";

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);


        public void LogInfo(string message)
        {
            var logMessage = $"INFO: {message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            WriteLogToFile("info.txt", logMessage);
        }


        public void LogWarning(string message)
        {
            var logMessage = $"WARNING: {message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            WriteLogToFile("warning.txt", logMessage);
        }


        public void LogError(string message, Exception ex)
        {
            var logMessage = $"ERROR: {message}. Exception: {ex.Message} ({DateTime.Now:yyyy-MM-dd HH:mm:ss.fff})";
            WriteLogToFile("error.txt", logMessage);
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
