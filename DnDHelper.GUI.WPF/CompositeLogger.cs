using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DnDHelper.Domain;

namespace DnDHelper.GUI.WPF
{
    public class CompositeLogger : ILogger
    {
        private readonly List<ILogger> _loggers;

        public CompositeLogger(string loggerName)
        {
            _loggers = new List<ILogger>();
            _loggers.Add(new ApplicationLogger(ServiceContainer.GetInstance<IAppAPI>(), loggerName));
            _loggers.Add(new Log4NetLogger(loggerName));
        }

        public void Info(string message)
        {
            _loggers.ForEach(l => l.Info(message));
        }

        public void Debug(string message)
        {
            _loggers.ForEach(l => l.Debug(message));
        }

        public void Error(string message, Exception exception)
        {
            _loggers.ForEach(l => l.Error(message, exception));
        }

        public void Fatal(string message, Exception exception)
        {
            _loggers.ForEach(l => l.Fatal(message, exception));
        }
    }
    
    internal class ApplicationLogger : ILogger
    {
        private readonly IAppAPI _app;
        private readonly string _loggerName;

        public ApplicationLogger(IAppAPI app, string loggerName)
        {
            _app = app;
            _loggerName = loggerName;
        }

        public void Info(string message)
        {
            _app.Log(message, _loggerName, MessageType.Info);
        }

        public void Debug(string message)
        {
            _app.Log(message, _loggerName, MessageType.Debug);
        }

        public void Error(string message, Exception exception)
        {
            _app.Log(message, _loggerName, MessageType.Error);
        }

        public void Fatal(string message, Exception exception)
        {
            _app.Log(message, _loggerName, MessageType.Error);
        }
    }
}
