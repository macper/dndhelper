using System;

namespace DnDHelper.Domain
{
    public interface ILogger
    {
        void Info(string message);
        void Debug(string message);
        void Error(string message, Exception exception);
        void Fatal(string message, Exception exception);
    }
}