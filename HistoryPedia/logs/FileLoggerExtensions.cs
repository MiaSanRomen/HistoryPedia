using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace HistoryPedia.logs
{
    public static class FileLoggerExtensions
    {
        public static ILoggerFactory AddFile(this ILoggerFactory factory, string allLogPath, string errorLogPath)
        {
            factory.AddProvider(new FileLoggerProvider(allLogPath, errorLogPath));
            return factory;
        }
    }
}
