using clawSoft.clawPDF.Threading;
using NLog;

namespace clawSoft.clawPDF.Startup
{
    internal class MainWindowStart : MaybePipedStart
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        internal override string ComposePipeMessage()
        {
            return "ShowMain|";
        }

        internal override bool StartApplication()
        {
            _logger.Debug("Starting main form");
            ThreadManager.Instance.StartMainWindowThread();

            return true;
        }
    }
}