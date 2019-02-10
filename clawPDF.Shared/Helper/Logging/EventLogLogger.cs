using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace clawSoft.clawPDF.Shared.Helper.Logging
{
    public class EventLogLogger : ILogger
    {
        public static string TraceLogLayout =
            "$[${level}] ${processid}-${threadid} (${threadname}) ${callsite}: ${message}";

        public static string ShortLogLayout =
            "[${level}] ${callsite}: ${message}";

        private readonly EventLogTarget _eventLogTarget;
        private readonly string _logName;
        private LoggingRule _loggingRule;

        public EventLogLogger(string source, string logName, LogLevel logLevel)
        {
            _logName = logName;

            var config = new LoggingConfiguration();

            _eventLogTarget = new EventLogTarget();
            _eventLogTarget.Name = "EventLogTarget";
            _eventLogTarget.Log = _logName;
            _eventLogTarget.Source = source;
            _eventLogTarget.MachineName = ".";

            _eventLogTarget.Layout = GetLayoutForLogLevel(logLevel);

            config.AddTarget("EventLogTarget", _eventLogTarget);

            _loggingRule = new LoggingRule("*", logLevel, _eventLogTarget);
            config.LoggingRules.Add(_loggingRule);

            LogManager.Configuration = config;
        }

        public void ChangeLogLevel(LogLevel logLevel)
        {
            _eventLogTarget.Layout = GetLayoutForLogLevel(logLevel);

            LogManager.Configuration.LoggingRules.Remove(_loggingRule);

            _loggingRule = new LoggingRule("*", logLevel, _eventLogTarget);
            LogManager.Configuration.LoggingRules.Add(_loggingRule);

            LogManager.Configuration.Reload();
        }

        public string GetLogPath()
        {
            return "eventvwr /c:" + _logName;
        }

        private Layout GetLayoutForLogLevel(LogLevel logLevel)
        {
            return logLevel == LogLevel.Trace ? TraceLogLayout : ShortLogLayout;
        }
    }
}