using NLog;

namespace clawSoft.clawPDF.Shared.Helper.Logging
{
    internal interface ILogger
    {
        void ChangeLogLevel(LogLevel logLevel);

        string GetLogPath();
    }
}