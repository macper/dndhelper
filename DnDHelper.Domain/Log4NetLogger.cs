using System;
using log4net;

namespace DnDHelper.Domain
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log4NetLogger;

        public Log4NetLogger(string loggerName)
        {
            _log4NetLogger = log4net.LogManager.GetLogger(loggerName);
        }

        public void Info(string message)
        {
            _log4NetLogger.Info(message);
        }

        public void Debug(string message)
        {
            _log4NetLogger.Debug(message);
        }

        public void Error(string message, Exception exception)
        {
            _log4NetLogger.Error(message, exception);
        }

        public void Fatal(string message, Exception exception)
        {
            _log4NetLogger.Fatal(message, exception);
        }
    }
}