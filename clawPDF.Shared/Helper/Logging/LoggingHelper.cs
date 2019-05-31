using System;
using clawSoft.clawPDF.Core.Settings.Enums;
using NLog;

namespace clawSoft.clawPDF.Shared.Helper.Logging
{
    /// <summary>
    ///     LoggingUtil provides functionality for setting up and managing the logging behavior.
    /// </summary>
    public class LoggingHelper
    {
        private static ILogger _logger;

        /// <summary>
        ///     Hiding constructor as there are only static methods
        /// </summary>
        private LoggingHelper()
        {
        }

        public static string LogFile
        {
            get
            {
                if (_logger == null) return null;
                return _logger.GetLogPath();
            }
        }

        public static void InitFileLogger(string applicationName, LoggingLevel loggingLevel, string logFilePath = null)
        {
            InitFileLogger(applicationName, GetLogLevel(loggingLevel), logFilePath);
        }

        public static void InitFileLogger(string applicationName, LogLevel logLevel, string logFilePath = null)
        {
            if (_logger == null)
                _logger = new FileLogger(applicationName, logLevel, logFilePath);
        }

        public static void InitEventLogLogger(string source, string logName, LoggingLevel loggingLevel)
        {
            InitEventLogLogger(source, logName, GetLogLevel(loggingLevel));
        }

        public static void InitEventLogLogger(string source, string logName, LogLevel logLevel)
        {
            if (_logger == null)
                _logger = new EventLogLogger(source, logName, logLevel);
        }

        public static void InitConsoleLogger(string applicationName, LoggingLevel loggingLevel)
        {
            InitConsoleLogger(applicationName, GetLogLevel(loggingLevel));
        }

        public static void InitConsoleLogger(string applicationName, LogLevel logLevel)
        {
            if (_logger == null)
                _logger = new ConsoleLogger(applicationName, logLevel);
        }

        private static void ChangeLogLevel(LogLevel logLevel)
        {
            if (_logger == null)
                throw new ArgumentException("Logging has not been initialized");

            _logger.ChangeLogLevel(logLevel);
        }

        public static void ChangeLogLevel(LoggingLevel loggingLevel)
        {
            ChangeLogLevel(GetLogLevel(loggingLevel));
        }

        private static LogLevel GetLogLevel(LoggingLevel loggingLevel)
        {
            return LogLevel.FromOrdinal((int)loggingLevel);
        }
    }
}