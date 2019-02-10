using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace clawSoft.clawPDF.Shared.Helper.Logging
{
    internal class ConsoleLogger : ILogger
    {
        public static string TraceLogLayout =
            "${shortdate} ${date:format=HH\\:mm\\:ss.ffff} [${level}] ${processid}-${threadid} (${threadname}) ${callsite}: ${message}";

        public static string ShortLogLayout =
            "${shortdate} ${date:format=HH\\:mm\\:ss.ffff} [${level}] ${callsite}: ${message}";

        private readonly ConsoleTarget _consoleLogTarget;
        private LoggingRule _loggingRule;

        public ConsoleLogger(string applicationName, LogLevel logLevel)
        {
            var config = new LoggingConfiguration();

            _consoleLogTarget = new ConsoleTarget();
            _consoleLogTarget.Name = "ConsoleLogTarget";

            _consoleLogTarget.Layout = GetLayoutForLogLevel(logLevel);

            config.AddTarget("ConsoleLogTarget", _consoleLogTarget);

            _loggingRule = new LoggingRule("*", logLevel, _consoleLogTarget);
            config.LoggingRules.Add(_loggingRule);

            LogManager.Configuration = config;
        }

        public void ChangeLogLevel(LogLevel logLevel)
        {
            _consoleLogTarget.Layout = GetLayoutForLogLevel(logLevel);

            LogManager.Configuration.LoggingRules.Remove(_loggingRule);

            _loggingRule = new LoggingRule("*", logLevel, _consoleLogTarget);
            LogManager.Configuration.LoggingRules.Add(_loggingRule);

            LogManager.Configuration.Reload();
        }

        public string GetLogPath()
        {
            return null;
        }

        private Layout GetLayoutForLogLevel(LogLevel logLevel)
        {
            return logLevel == LogLevel.Trace ? TraceLogLayout : ShortLogLayout;
        }
    }
}