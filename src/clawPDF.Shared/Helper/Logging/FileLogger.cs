using System;
using System.IO;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace clawSoft.clawPDF.Shared.Helper.Logging
{
    internal class FileLogger : ILogger
    {
        public static string TraceLogLayout =
            "${shortdate} ${date:format=HH\\:mm\\:ss.ffff} [${level}] ${processid}-${threadid} (${threadname}) ${callsite}: ${message} ${exception:innerFormat=type,message:maxInnerExceptionLevel=1:format=tostring}";

        public static string ShortLogLayout =
            "${shortdate} ${date:format=HH\\:mm\\:ss.ffff} [${level}] ${callsite}: ${message} ${exception:innerFormat=type,message:maxInnerExceptionLevel=1:format=tostring}";

        private readonly FileTarget _fileTarget;
        private readonly string _logFile;
        private LoggingRule _loggingRule;

        public FileLogger(string applicationName, LogLevel logLevel, string logFilePath = null)
        {
            var config = new LoggingConfiguration();

            _fileTarget = new FileTarget();
            _fileTarget.Layout = ShortLogLayout;
            _fileTarget.ArchiveAboveSize = 10485760; // 10MB
            _fileTarget.MaxArchiveFiles = 2;

            _fileTarget.Layout = GetLayoutForLogLevel(logLevel);

            _logFile = GetLogFilePathAndMakeSureDirectoryExists(applicationName, logFilePath);
            _fileTarget.FileName = _logFile;

            config.AddTarget("file", _fileTarget);

            _loggingRule = new LoggingRule("*", logLevel, _fileTarget);
            config.LoggingRules.Add(_loggingRule);

            LogManager.Configuration = config;
        }

        public void ChangeLogLevel(LogLevel logLevel)
        {
            _fileTarget.Layout = GetLayoutForLogLevel(logLevel);

            LogManager.Configuration.LoggingRules.Remove(_loggingRule);

            _loggingRule = new LoggingRule("*", logLevel, _fileTarget);
            LogManager.Configuration.LoggingRules.Add(_loggingRule);

            LogManager.Configuration.Reload();
        }

        public string GetLogPath()
        {
            return _logFile;
        }

        private Layout GetLayoutForLogLevel(LogLevel logLevel)
        {
            return logLevel == LogLevel.Trace ? TraceLogLayout : ShortLogLayout;
        }

        private string GetLogFilePathAndMakeSureDirectoryExists(string applicationName, string logFilePath)
        {
            if (Path.GetDirectoryName(logFilePath) == null) logFilePath = GetDefaultLogFilePath(applicationName);

            var dir = Path.GetDirectoryName(logFilePath);

            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            return logFilePath;
        }

        private string GetDefaultLogFilePath(string applicationName)
        {
            var logFileDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                applicationName);
            var fileName = Path.Combine(logFileDir, applicationName + ".log");
            return fileName;
        }
    }
}