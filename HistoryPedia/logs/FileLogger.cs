using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HistoryPedia.logs
{
    public class FileLogger : ILogger
    {
        private string allLogPath;
        private string errorLogPath;
        private static object _lock = new object();
        public FileLogger(string allLogPath, string errorLogPath)
        {
            this.allLogPath = allLogPath;
            this.errorLogPath = errorLogPath;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null)
            {
                lock (_lock)
                {
                    File.AppendAllText(allLogPath, formatter(state, exception) + Environment.NewLine);
                    if (logLevel == LogLevel.Error || logLevel == LogLevel.Warning || logLevel == LogLevel.Critical)
                    {
                        File.AppendAllText(errorLogPath, formatter(state, exception) + Environment.NewLine);
                    }
                }
            }
        }
    }
}
