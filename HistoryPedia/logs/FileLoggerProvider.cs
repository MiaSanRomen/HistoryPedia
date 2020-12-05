using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HistoryPedia.logs
{
    public class FileLoggerProvider : ILoggerProvider
    {
        private string allLogPath;
        private string errorLogPath;
        public FileLoggerProvider(string allLogPath, string errorLogPath)
        {
            this.allLogPath = allLogPath;
            this.errorLogPath = errorLogPath;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(allLogPath, errorLogPath);
        }

        public void Dispose()
        {
        }
    }
}
